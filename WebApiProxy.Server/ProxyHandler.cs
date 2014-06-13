using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApiProxy.Server.Templates;
//testing ci
namespace WebApiProxy.Server
{
    public class ProxyHandler : DelegatingHandler
    {
        private MetadataProvider _metadataProvider;
        public ProxyHandler()
        {
            _metadataProvider = new MetadataProvider();
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {



            return await Task.Run(() =>
            {

                var metadata = _metadataProvider.GetMetadata(request);

                if (request.Headers.Any(h => h.Key == "X-Proxy-Type" && h.Value.Contains("metadata")))
                    return request.CreateResponse(System.Net.HttpStatusCode.OK, metadata);


                var template = new JsProxyTemplate(metadata);
                var js = new StringContent(template.TransformText());

                js.Headers.ContentType = new MediaTypeHeaderValue("application/javascript");

                return new HttpResponseMessage { Content = js }; ;





            });

        }



    }
}
