using System;
using System.Net.Http;
using WebApiProxy.Core.Models;
using WebApiProxy.Tasks.Models;
using WebApiProxy.Tasks.Templates;

namespace WebApiProxy.Tasks.Infrastructure
{
    public class CSharpGenerator
    {
        private readonly Configuration config;
        public CSharpGenerator(Configuration config)
        {
            this.config = config;
        }

        public string Generate()
        {
            var metaData = GetProxy();
            var template = new CSharpProxyTemplate(config,metaData);
            var source = template.TransformText();
            return source;
        }


        private Metadata GetProxy()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Proxy-Type", "metadata");
                var response = client.GetAsync(config.Endpoint).Result;
                response.EnsureSuccessStatusCode();
                var metadata = response.Content.ReadAsAsync<Metadata>().Result;
                return metadata;
            }
        }
    }
}
