using System.Web.Http;

using DS.Kids.API.App_Start;

using Microsoft.Practices.Unity;

using Newtonsoft.Json;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression;
using Microsoft.AspNet.WebApi.MessageHandlers.Compression.Compressors;
using GoogleAnalyticsTracker.WebApi;

namespace DS.Kids.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ActionTrackingAttribute("UA-446223-49", "www.dskids.com.br"));
            
            var container = new UnityContainer();
            UnityConfig.RegisterAllTypes(container);
            config.DependencyResolver = new UnityResolver(container);

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            
            config.Routes.MapHttpRoute(
                name: "Imagens",
                routeTemplate: "v1/imagens/{entidade}/{hash}",
                defaults: new { controller = "imagens", action = "get", entidade = RouteParameter.Optional, hash = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "Dica",
                routeTemplate: "v1/categorias/{idCategoria}/dicas",
                defaults: new { controller = "categorias", action = "GetDicas", idCategoria = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            GlobalConfiguration.Configuration.MessageHandlers.Insert(0, new ServerCompressionHandler(new GZipCompressor(), new DeflateCompressor()));
        }

    }
}