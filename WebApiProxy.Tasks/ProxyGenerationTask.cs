using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using WebApiProxy.Core.Models;
using WebApiProxy.Tasks.Templates;

namespace WebApiProxy.Tasks
{
    public class ProxyGenerationTask : ITask
    {
        private Configuration config;

        private const string DefaultFilePath = "WebApiProxySource.cs";

        [Output]
        public string Filename { get; set; }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        public bool Execute()
        {
            config = Configuration.Load();
            config.FilePath = config.FilePath ?? DefaultFilePath;
            var fileDirectory = new FileInfo(config.FilePath).Directory.FullName;
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            var cacheFilePath = config.FilePath + ".cache";

            string source;
            try
            {
                config.Metadata = GetProxy();
                var template = new CSharpProxyTemplate(config);
                source = template.TransformText();

                File.WriteAllText(cacheFilePath, source);
            }
            catch (InvalidOperationException)
            {
                source = File.ReadAllText(cacheFilePath);
            }

            File.WriteAllText(config.FilePath, source);

            this.Filename = config.FilePath;

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

                    var response = Task.Run(() => client.GetAsync(config.Endpoint)).Result;
                    response.EnsureSuccessStatusCode();

                    var metadata = response.Content.ReadAsAsync<Metadata>().Result;

                    return metadata;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(config.Endpoint, ex);
            }
        }
    }
}

