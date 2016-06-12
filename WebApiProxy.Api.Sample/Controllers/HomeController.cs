using System.Web.Mvc;
using WebApiProxy.Tasks;

namespace WebApiProxy.Api.Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var task = new ProxyGenerationTask()
                       {
                           Root = Server.MapPath("~/ProxyFiles")
            };
            var res = task.Execute();


            return Content("generate done!");
        }
    }
}
