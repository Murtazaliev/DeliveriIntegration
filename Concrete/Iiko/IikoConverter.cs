using Delivery.SelfServiceKioskApi.Domain;
using Delivery.SelfServiceKioskApi.Models.Delivery;
using Delivery.SelfServiceKioskApi.Models.Delivery.Order;
using Delivery.SelfServiceKioskApi.Models.Iiko.Order;
using Delivery.SelfServiceKioskApi.Models.Iiko.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.SelfServiceKioskApi.Helpers;

namespace Delivery.SelfServiceKioskApi.Concrete.Iiko
{
    public class IikoConverter : IConverter
    {
        List<ProductCategory> Categories = new List<ProductCategory>();

        public override string ConverterResponseNomenclatureData<T>(T entity, DateTime createRequestDate, DateTime createResponseDate, Guid partnerId,
            Guid requestId, Guid categoryId)
        {
            var data = entity as IikoNomenclatureViewModel;
            List<Models.Iiko.Images> images = new List<Models.Iiko.Images>();
            string imgUrl;
            try
            {
                List<ProductCategory> nomenclature = new List<ProductCategory>();


                //TODO Изменить Take(10)
                foreach (var item in data.groups.ToList())
                {
                    images = item.images;
                    imgUrl = item.images.Count != 0 ? item.images[0].imageUrl : "";
                    ProductCategory group = new ProductCategory()
                    {
                        Id = Guid.Parse(item.id),
                        Img = imgUrl,
                        Name = item.name,
                        ParentId = item.parentGroup == null ? Guid.Empty : Guid.Parse(item.parentGroup),
                        Products = new List<Product>()
                    };
                    for (int i = 0; i < data.products.Where(x => x.parentGroup == item.id).Count(); i++)
                    {
                        var subItem = data.products.Where(x => x.parentGroup == item.id).ToList()[i];
                        if(subItem.type == "modifier")
                            continue;

                        imgUrl = subItem.images.Count != 0 ? subItem.images[0].imageUrl : "";
                        Product product = new Product()
                        {
                            Cost = (decimal) subItem.price,
                            Description = subItem.description != "" ? subItem.description : null,
                            Id = subItem.id,
                            Img = imgUrl,
                            Name = subItem.name,
                            IsVisible = (!subItem.isDeleted)??false,
                            PortionSize = subItem.weight.ToString(),
                        };
                        if (partnerId == Organisations.PaulowniaId) // Модификаторы в номенклатуре павлонии
                        {
                            foreach (var additiveGroup in subItem.groupModifiers.ToList())
                            {
                                if (additiveGroup.modifierId == null)
                                    continue;

                                var groupToAdd = data.groups.FirstOrDefault(x => x.id == additiveGroup.modifierId.ToString());
                                var requiredAdditiveGroup = new RequiredAdditiveGroup();
                                requiredAdditiveGroup.Id = Guid.NewGuid();
                                requiredAdditiveGroup.ModifierId = additiveGroup.modifierId ?? Guid.Empty;
                                requiredAdditiveGroup.Name = groupToAdd.name;
                                requiredAdditiveGroup.Additives = additiveGroup.childModifiers.Select(additiveProduct =>
                                {
                                    var product = data.products.FirstOrDefault(n => n.id == additiveProduct.modifierId);

                                    var additive = new Additive();
                                    additive.Id = Guid.NewGuid();
                                    additive.ModifierId = additiveProduct.modifierId ?? Guid.Empty;
                                    additive.Cost = Convert.ToDecimal(product.price);
                                    additive.Name = product.name;

                                    return additive;
                                }).ToList();
                                product.RequiredAdditiveGroups.Add(requiredAdditiveGroup);
                            }
                        }
                        group.Products.Add(product);
                    }
                    nomenclature.Add(group);
                }

                GetPartnerProductsResponseData responseData = new GetPartnerProductsResponseData()
                {
                    CreateRequestDate = createRequestDate,
                    CreateResponseDate = createResponseDate,
                    PartnerId = partnerId,
                    RequestId = requestId,
                    PartnerName = "ХЗ",
                    ProductCategories = nomenclature
                };
                if (categoryId != Guid.Empty)
                {
                    Categories = new List<ProductCategory>();
                    ResponseModel(responseData, categoryId);
                    responseData.ProductCategories = Categories;
                }

                return JsonConvert.SerializeObject(responseData);
            }
            catch (Exception e)
            {
                var x = images;
                return e.Message;
            }
        }

        public override string ConverterOrderForKiosk(CreateOrderRequestData data)
        {
            Root root = new Root();
            root.organization = data.PartnerId.ToString();
            root.customer = new Models.Iiko.Order.Customer
            {
                id = data.customer.Id.ToString(),
                name = data.customer.Name,
                phone = data.customer.Phonenumber
            };
            root.order = new IikoOrder
            {
                date = data.order.CreateDatetime.ToString("yyyy-MM-dd HH:mm:ss"),
                id = Guid.NewGuid().ToString(),
                isSelfService = "false",
                phone = data.customer.Phonenumber,
                MarketingSource = data.order.MarketingSource,
                MarketingSourceId = data.order.MarketingSourceId
            };

            if (data.order.PaymentItems != null)
                root.order.paymentItems.AddRange(data.order.PaymentItems);

            root.order.address = new Address
            {
                apartment = data.DeliveryLocation.Apartment,
                city = data.DeliveryLocation.City,
                home = data.DeliveryLocation.Home,
                street = data.DeliveryLocation.Street,
                comment = data.DeliveryLocation.Comment
            };

            root.order.items = new List<Item>();
            foreach (var item in data.OrderItems)
            {
                var product = new Item
                {
                    amount = item.Amount,
                    id = item.Id.ToString(),
                    name = item.Name,
                    sum = item.Sum,
                };

                if (item.Additives.Count > 0)
                    product.modifiers = item.Additives;
                root.order.items.Add(product);
            }

            return JsonConvert.SerializeObject(root);
        }
    }
}