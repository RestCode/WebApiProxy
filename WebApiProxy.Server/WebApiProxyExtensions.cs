using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace WebApiProxy.Server
{
    public static class WebApiProxyExtensions
    {
        /// <summary>
        /// Sets up the proxy route table entries.
        /// </summary>
        public static void RegisterProxyRoutes(this HttpConfiguration config, string routeTemplate = "api/proxies")
        {
            config.Routes.MapHttpRoute(
            name: "WebApiProxy",
            routeTemplate: routeTemplate,
            defaults: new { id = RouteParameter.Optional },
            constraints: null,
            handler: new ProxyHandler(config) { InnerHandler = new HttpControllerDispatcher(config) });
        }
    }
}
