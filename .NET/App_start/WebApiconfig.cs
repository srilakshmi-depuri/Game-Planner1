using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GamePlanner1WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes
            .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            // Enable CORS globally

            var cors = new EnableCorsAttribute("*", "*", "*"); // Allow requests from all origins, headers, and methods
            config.EnableCors(cors);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}", // Include {action} placeholder
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
