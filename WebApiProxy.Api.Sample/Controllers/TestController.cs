using System.Web.Http;
using System.Web.Http.Description;

namespace WebApiProxy.Api.Sample.Controllers
{
    [RoutePrefix("api/test")]
   public class TestController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(GenericBase<string>))]
        [Route("GetFromSimpleArg")]
        public IHttpActionResult GetFromSimpleArg(string id)
        {
            return Ok(new GenericBase<string>() {Id = id});
        }

        [HttpGet]
        [ResponseType(typeof(ComplexModel))]
        [Route("GetFromComplexArg")]
        public IHttpActionResult GetFromComplexArg([FromUri]ComplexModel dataArg)
        {
            return Ok(dataArg);
        }

        [HttpGet]
        [ResponseType(typeof(NestedModel))]
        [Route("GetFromMixedArg")]
        public IHttpActionResult GetFromMixedArg(int id, [FromUri] ComplexModel dataArg)
        {
            return Ok(new NestedModel()
            {
                Id = id,
                ComplexModel = dataArg
            });
        }


        [HttpPost]
        [Route("PostFromMixedArg")]
        public TotalResult PostFromMixedArg(string simpleStr,[FromUri] NestedModel uriNestedArg,[FromBody] ComplexModel bodyComplexArg)
        {
            /*
                this will not work in proxy client, because uriNestedArg will not generate 
                a uri for client to post,but it can be support in proxyTemplate.tt 
                if nested class[array,list...] is used for uri parameters binding become more common. 

                but this can be request from browser[post uri with body part], it means this Action has no problem.
             */


            return new TotalResult()
            {
                SimpleStr = simpleStr,
                ComplexModel = bodyComplexArg,
                NestedModel = uriNestedArg
            };
        }


        [HttpPost]
        [Route("PostFromMixedArg2")]
        public TotalResult PostFromMixedArg2(string simpleStr, [FromUri]ComplexModel uriComplexArg, [FromBody]NestedModel bodyNestedArg)
        {
            return new TotalResult()
            {
                SimpleStr = simpleStr,
                ComplexModel = uriComplexArg,
                NestedModel = bodyNestedArg
            };
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

    public class GenericBase<T>
    {
        public T Id { get; set; }
    }

    public class ComplexModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class NestedModel:GenericBase<int>
    {
        public ComplexModel ComplexModel { get; set; }
    }

    public class TotalResult
    {
        public string SimpleStr { get; set; }
        public ComplexModel ComplexModel { get; set; }
        public NestedModel NestedModel { get; set; }
    }
}
