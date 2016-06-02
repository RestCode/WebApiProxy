using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebApiProxy.Tasks.Infrastructure;
using WebApiProxy.Tasks.Models;

namespace WebApiProxy.Api.Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            var config = Configuration.Load(Server.MapPath("~"));
            var generator = new CSharpGenerator(config);
            var source = generator.Generate();
            var filePath = Server.MapPath("Controllers/GenerateClient.cs");
            System.IO.File.WriteAllText(filePath, source);


            return Content(source);
        }
    }
}
