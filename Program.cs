using Delivery.SelfServiceKioskApi.Concrete;
using Delivery.SelfServiceKioskApi.DbModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Delivery.SelfServiceKioskApi
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            DeliveryService delivery = new DeliveryService();
            Thread thread = new Thread(new ThreadStart(delivery.RunRequests));
            thread.Start();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
