using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApiProxy.Api.Sample.Controllers
{
    [RoutePrefix("api/test")]
   public class TestController : ApiController
    {
        [ResponseType(typeof(void))]
        [Route("case1")]
        public IHttpActionResult Case1()
        {
            return Ok("even the return type is void");
        }

        [ResponseType(typeof(Case2Model))]
        [Route("case2")]
        public IHttpActionResult Case2(Case2Model dataFrom)
        {
            return Ok();
        }

        [ResponseType(typeof(Case3Model))]
        [Route("case3")]
        public IHttpActionResult Case3(Case3Model dataFrom)
        {
            return Ok();
        }


        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class Case2Model
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Case3Model
    {
        

    }
}
