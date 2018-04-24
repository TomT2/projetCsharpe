using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Isen.DotNet.Library.Data;
using Isen.DotNet.Library.Repositories.DbContext;
using Isen.DotNet.Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Isen.DotNet.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Utiliser Entity Framework
            services.AddDbContext<ApplicationDbContext>(options => 
                // Utiliser le provider Sqlite
                options.UseSqlite(
                    // Utiliser la clé DefaultConnection
                    // du fichier de config
                    Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.ReferenceLoopHandling =
                        ReferenceLoopHandling.Ignore;
                });
            
            // Injection de dépendances
            //----------------------

            // Injection des repo
            services.AddScoped<ICityRepository, DbContextCityRepository>();
            services.AddScoped<IPersonRepository, DbContextPersonRepository>();

            // injection d'autres services
            services.AddScoped<SeedData>();

            // AddTransient : nouvelle référence à chaque appel
            // AddSingleton : même référence pour toute l'appli, y compris
            //        entre différents appels http
            // AddScoped : même référence mais limitée à la durée de vie
            //     d'un appel http
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
