using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using Microsoft.Extensions.Configuration.Memory;
using System.Configuration;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Specialized;
using Autofac.Extensions.DependencyInjection;
using DddEfSample.Infrastructure.EntityFramework.Flights;
using DddEfSample.Domain.Flights;
using DddEfSample.Infrastructure.EntityFramework.Flights.Views;
using DddEfSample.Domain.Flights.Views;
using System.Data.Entity;

namespace DddEfSample.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddConfigurationManager()
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(o => o.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(o =>
            {
                o.EnableForHttps = true;
                o.MimeTypes = new[] { "application/json", "application/javascript", "text/html", "text/css" };
                o.Providers.Add<GzipCompressionProvider>();
            });
            
            services.AddMvcCore()
                .AddControllersAsServices()
                .AddRazorViewEngine()
                .AddJsonFormatters(s =>
                {
                    s.Converters.Add(new StringEnumConverter());
                    s.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddApiExplorer();
            services.AddOptions();
            services.AddLogging();
            
            var containerBuilder = new ContainerBuilder();
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<FlightDbContext>());
            containerBuilder.RegisterType<FlightDbContext>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<FlightRepository>().As<IFlightRepository>();
            containerBuilder.RegisterType<FlightSummaryView>().As<IFlightSummaryView>();
            
            containerBuilder.Populate(services);
            ApplicationContainer = containerBuilder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = false // Aurelia Webpack Plugin HMR currently has issues. Leave this set to false.
                });
            }
            
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            applicationLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
    
    public static class Extensions
    {
        public static IConfigurationBuilder AddConfigurationManager(this IConfigurationBuilder builder)
        {
            var source = new MemoryConfigurationSource();
            var appSettings = ConfigurationManager.AppSettings.AllKeys.ToDictionary(k => "AppSettings:" + k, k => ConfigurationManager.AppSettings[k]);
            var connectionStrings = ConfigurationManager.ConnectionStrings.OfType<ConnectionStringSettings>().ToDictionary(s => "ConnectionStrings:" + s.Name, s => s.ConnectionString);
            source.InitialData = appSettings.Concat(connectionStrings);
            return builder.Add(source);
        }
    }
}
