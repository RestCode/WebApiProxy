using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiProxy.Server;

namespace WebApiProxy.Api.Sample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务



            // Web API 路由

            config.RegisterProxyRoutes();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
