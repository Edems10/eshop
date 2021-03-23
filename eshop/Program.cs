﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using eshop.Models.Database;
using Serilog;
using Microsoft.Extensions.Hosting;

namespace eshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost webHost = CreateWebHostBuilder(args).Build();

            using (var scope = webHost.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var dbContext = serviceProvider.GetRequiredService<EshopDBContext>();
                DbInitializer.Initialize(dbContext);
            }

            DbInitializer.EnsureRoleCreated(webHost.Services);
            DbInitializer.EnsureAdminCreated(webHost.Services);
            webHost.Run();


        }




        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddEventSourceLogger();
                logging.AddEventLog();
                logging.AddFile("Logs/eshop-{Date}.txt");
                //logging.AddConsole();
            });



    }
}
