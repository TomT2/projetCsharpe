using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Isen.DotNet.Library.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Isen.DotNet.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
                    // Read in file with File class.


            var host = BuildWebHost(args);
            // Récupérer une instance de SeedData
            // en appelant le moteur d'injection de dépendances
            using (var scope = host.Services.CreateScope())
            {
                var seed = scope.ServiceProvider
                    .GetService<SeedData>();
                seed.DropDatabase();
                seed.CreateDatabase();
                seed.AddDepartments();
                seed.AddCities();
                seed.AddAddresses();
                seed.AddCategories();
                //seed.AddInterestPoints();
                //seed.AddPersons();
                
            }


            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
