using System;
using System.IO;
using System.Net.Http;
using Microsoft.Build.Framework;
using WebApiProxy.Core.Models;
using WebApiProxy.Tasks.Templates;
using WebApiProxy.Tasks.Models;
using Microsoft.Build.Evaluation;
using System.Linq;
namespace WebApiProxy.Tasks
{
    public class ProxyGenerationTask : ITask
    {
        private Configuration config;

        [Output]
        public string Filename { get; set; }

        [Output]
        public string Root { get; set; }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        public bool Execute()
        {
            try
            {

                if (!File.Exists(Filename))
                {
                    File.WriteAllText(Filename, "//WebApiProxy generation is disabled");
                }
                config = Configuration.Load(Root);

                if (config.GenerateOnBuild)
                {
                    config.Metadata = GetProxy();
                    var template = new CSharpProxyTemplate(config);
                    var source = template.TransformText();
                    File.WriteAllText(Filename, source);
                    File.WriteAllText(Configuration.CacheFile, source);
                    
                }
            }
            catch (ConnectionException)
            {
                tryReadFromCache();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }



        private Metadata GetProxy()
        {
            var url = string.Empty;

            try
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
            catch (Exception ex)
            {

                throw new ConnectionException(config.Endpoint);
            }
        }

        private void tryReadFromCache()
        {
            if (!File.Exists(Configuration.CacheFile))
            {
                throw new ConnectionException(config.Endpoint);
            }
            var source = File.ReadAllText(Configuration.CacheFile);
            File.WriteAllText(Filename, source);
        }


    }
}

