using Delivery.SelfServiceKioskApi.Concrete.Iiko;
using Delivery.SelfServiceKioskApi.DbModel;
using Delivery.SelfServiceKioskApi.Domain;
using Delivery.SelfServiceKioskApi.Models.Delivery;
using Delivery.SelfServiceKioskApi.Models.Delivery.Order;
using Delivery.SelfServiceKioskApi.Models.Iiko.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Concrete
{
    public class DeliveryService : IDelivery
    {
        IConverter _converter;
        IKiosk _kiosk;
        DeliveryKioskApiContext _context;
        public DeliveryService()
        {
            _context = new DeliveryKioskApiContext();
        }

        List<Product> Products = new List<Product>();



        /// <summary>
        /// Запись запроса в очередь
        /// </summary>
        /// <param name="organization_id"></param>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="kiosk_name"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task AddRequest(RequestParamsModel paramsModel)
        {
            await _context.AddAsync(new QueueRequest
            {
                Id = Guid.NewGuid(),
                Code = paramsModel.RequestId,
                IdOrganization = paramsModel.organizationId,
                Login = paramsModel.Login,
                Password = paramsModel.Password,
                RequestName = "Запрос номенклатуры",
                RequestDate = DateTime.Now,
                IsProcessed = false,
                Description = (int)paramsModel.KioskId,
                IdCategory = paramsModel.CategoryId
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Возврат результата в CRON Delivery
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetNomenclature(Guid code)
        {
            lock (_context)
            {


                var request = _context.QueueRequests.FirstOrDefault(x => x.Code == code);
                if (request != null)
                {
                    if (request.IsProcessed == true)
                    {
                        return request.Answer;
                    }
                }
                return JsonConvert.SerializeObject("Запрос не выполнен, повторите попытку позже!");
            }
        }
        public void RunRequests()
        {
            while (true)
            {
                RequestNomenclature().Wait();
            }
        }
        /// <summary>
        /// Получение результата запроса из киоска
        /// </summary>
        /// <returns></returns>
        public async Task RequestNomenclature()
        {
            var request = await _context.QueueRequests?.OrderBy(x => x.RequestDate).FirstOrDefaultAsync(x => x.IsProcessed == false);
            if (request != null)
            {
                switch ((KioskName)request.Description)
                {
                    case KioskName.Iiko:
                        _kiosk = new IikoService();
                        break;
                    default:
                        _kiosk = new IikoService();
                        break;
                }
                var access_token = JsonConvert.SerializeObject(_kiosk.Authorize(request.Login, request.Password));
                var response = Task.Run(() => _kiosk.GetNomenclature(request.IdOrganization, access_token)).Result;


                var result = Converter(response, request.Description, request.RequestDate, request.AnswerDate ?? DateTime.Now, request.IdOrganization ?? Guid.Empty, request.Code ?? Guid.Empty, request.IdCategory ?? Guid.Empty);

                request.Answer = result;
                request.AnswerDate = DateTime.Now;
                request.IsProcessed = true;
                await _context.SaveChangesAsync();



            }
            Thread.Sleep(500);
        }

        /// <summary>
        /// Преобразование модели киоска в модель Cron market
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="type"></param>
        /// <param name="createRequestDate"></param>
        /// <param name="createResponseDate"></param>
        /// <param name="partnerId"></param>
        /// <param name="requestId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string Converter(string entity, int type, DateTime createRequestDate, DateTime createResponseDate, Guid partnerId, Guid requestId, Guid categoryId)
        {
            try
            {
                switch ((KioskName)type)
                {
                    case KioskName.Iiko:
                        var iikoQuery = JsonConvert.DeserializeObject<IikoNomenclatureViewModel>(entity);
                        _converter = new IikoConverter();
                        return _converter.ConverterResponseNomenclatureData(iikoQuery, createRequestDate, createResponseDate, partnerId, requestId, categoryId);

                    default:
                        _converter = new IikoConverter();
                        iikoQuery = JsonConvert.DeserializeObject<IikoNomenclatureViewModel>(entity);
                        return _converter.ConverterResponseNomenclatureData(iikoQuery, createRequestDate, createResponseDate, partnerId, requestId, categoryId);

                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddOrder(CreateOrderRequestData data)
        {
            
            try
            {
                switch ((KioskName)data.Kiosk)
                {
                    case KioskName.Iiko:
                        _converter = new IikoConverter();
                        _kiosk = new IikoService();
                        string token = _kiosk.Authorize(data.Login, data.Password);
                        var root = _converter.ConverterOrderForKiosk(data);
                        return _kiosk.AddOrderAsync(data.PartnerId, token, root).Result;

                    default:
                        _converter = new IikoConverter();
                        _kiosk = new IikoService();
                        token = _kiosk.Authorize(data.Login, data.Password);
                        root = _converter.ConverterOrderForKiosk(data);
                        return _kiosk.AddOrderAsync(data.PartnerId, token, root).Result;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}