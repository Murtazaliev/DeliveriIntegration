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
using Delivery.SelfServiceKioskApi.Concrete.Rkeeper;
using Delivery.SelfServiceKioskApi.Models.GreenApple;
using System.IO;
using Aspose.Zip.Rar;
using System.Text;

namespace Delivery.SelfServiceKioskApi.Concrete
{
    public class GreenAppleService
    {
        DeliveryKioskApiContext _context;
        public GreenAppleService()
        {
            _context = new DeliveryKioskApiContext();
        }

        public void SaveNomenclature(NomenclatureRequestData model)
        {
            using (var compressedFileStream = new MemoryStream())
            {
                model.NomenclatureFile.CopyTo(compressedFileStream);
                using (var zipArchive = new RarArchive(compressedFileStream))
                {
                    foreach (var caseAttachmentModel in zipArchive.Entries)
                    {
                        using (StreamReader streamReader = new StreamReader(caseAttachmentModel.Open(), Encoding.Default))
                        {
                            var json = streamReader.ReadToEnd();
                            
                        }
                    }
                }
            }
        }
    }
}