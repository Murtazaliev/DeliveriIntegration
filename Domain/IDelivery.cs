using Delivery.SelfServiceKioskApi.Models.Delivery;
using Delivery.SelfServiceKioskApi.Models.Delivery.Order;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi.Domain
{
    interface IDelivery
    {
        Task AddRequest(RequestParamsModel paramsModel);
        Task RequestNomenclature();
        string GetNomenclature(Guid code);
        Task<string> AddOrder(CreateOrderRequestData data);

    }
}
