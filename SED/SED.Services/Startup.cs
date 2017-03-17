using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;
using Owin;
using SED.DAL;
using System;
using System.Web.Http;

namespace SED.Services
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use(async (env, next) =>
            {
                Console.WriteLine(string.Concat("Http method: ", env.Request.Method, ", path: ", env.Request.Path));
                await next();
                Console.WriteLine(string.Concat("Response code: ", env.Response.StatusCode));
            });

            RunWebApiConfiguration(appBuilder);            
        }

        private static void RunWebApiConfiguration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            Register(config);
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            ConfigureRoutes(config);
            appBuilder.UseWebApi(config);
        }

        private static void ConfigureRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}