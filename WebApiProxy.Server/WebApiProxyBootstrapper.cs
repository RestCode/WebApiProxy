using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using WebApiProxy.Server;

//[assembly: PreApplicationStartMethod(typeof(WebApiProxyBootstrapper), "RegisterProxyRoutes")]
//from now, this method need to call explicite
namespace WebApiProxy.Server
{
    public static class WebApiProxyBootstrapper
    {
        /// <summary>
        /// Sets up the proxy route table entries.
        /// </summary>
        public static void RegisterProxyRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
            name: "WebApiProxy",
            routeTemplate: "api/proxies",
            defaults: new { id = RouteParameter.Optional },
            constraints: null,
            handler: new ProxyHandler(config) { InnerHandler = new HttpControllerDispatcher(config) }
        );


        }
    }
}
