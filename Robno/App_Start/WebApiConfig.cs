using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Robno
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //dodano da se ukloni serijalizacija + JSON

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
            Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.Formatters.Remove(config.Formatters.XmlFormatter);
    

        }
    }
}
