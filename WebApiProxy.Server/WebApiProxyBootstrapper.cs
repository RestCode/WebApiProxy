using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using WebApiProxy.Server;

[assembly: PreApplicationStartMethod(typeof(WebApiProxyBootstrapper), "RegisterProxyRoutes")]

namespace WebApiProxy.Server
{
    public static class WebApiProxyBootstrapper
    {
        /// <summary>
        /// Sets up the proxy route table entries.
        /// </summary>
        public static void RegisterProxyRoutes()
        {
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
            name: "WebApiProxy",
            routeTemplate: "api/proxies",
            defaults: new { id = RouteParameter.Optional },
            constraints: null,
            handler: new ProxyHandler() { InnerHandler = new HttpControllerDispatcher(GlobalConfiguration.Configuration) }
        );


        }
    }
}
