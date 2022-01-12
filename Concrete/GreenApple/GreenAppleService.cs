using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Zip.Rar;
using Delivery.SelfServiceKioskApi.DbModel;
using Delivery.SelfServiceKioskApi.Helpers;
using Delivery.SelfServiceKioskApi.Models.GreenApple;
using Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels;
using Newtonsoft.Json;
using Product = Delivery.SelfServiceKioskApi.Models.GreenApple.GreenAppleModels.Product;

namespace Delivery.SelfServiceKioskApi.Concrete.GreenApple
{
    public class GreenAppleService
    {
        private DeliveryKioskApiContext _dbContext;
        private GreenAppleConverter _converter;
        public GreenAppleService(DeliveryKioskApiContext dbContext)
        {
            _dbContext = dbContext;
            _converter = new GreenAppleConverter();
        }

        public async Task SaveNomenclature(NomenclatureRequestData model)
        {
            List<Section> sections = new List<Section>();
            List<Category> categories = new List<Category>();
            List<Product> products = new List<Product>();

            await using var zipFileStream = new MemoryStream();
            await model.NomenclatureFile.CopyToAsync(zipFileStream);

            await using var compressedFileStream = new MemoryStream(zipFileStream.ToArray());
            using var zipArchive = new RarArchive(compressedFileStream);
            foreach (var caseAttachmentModel in zipArchive.Entries)
            {
                using var streamReader = new StreamReader(caseAttachmentModel.Open(), Encoding.Default);
                var json = await streamReader.ReadToEndAsync();
                if (caseAttachmentModel.Name == GreenAppleFileNames.Sections)
                    sections = JsonConvert.DeserializeObject<List<Section>>(json);
                else if (caseAttachmentModel.Name == GreenAppleFileNames.Categories)
                    categories = JsonConvert.DeserializeObject<List<Category>>(json);
                else if (caseAttachmentModel.Name == GreenAppleFileNames.Products)
                    products = JsonConvert.DeserializeObject<List<Product>>(json);
            }
            
            var result = await _converter.ConvertNomenclatureAsync(sections, categories, products);

            var request = new QueueRequest()
            {
                Id = Guid.NewGuid(),
                RequestName = "Номенклатура зеленого яблока",
                RequestDate = DateTime.Now,
                IsProcessed = false,
                IdOrganization = Organisations.GreenAppleId, // Зеленое яблоко
                Answer = JsonConvert.SerializeObject(result),
            };
            await _dbContext.QueueRequests.AddAsync(request);
            await _dbContext.SaveChangesAsync();
        }
        
        public string GetNomenclature()
        {
            var request = _dbContext.QueueRequests
                .OrderBy(n=>n.RequestDate)
                .FirstOrDefault(n => n.IdOrganization == Organisations.GreenAppleId && n.IsProcessed == false);
            return request?.Answer;
        }
    }
}