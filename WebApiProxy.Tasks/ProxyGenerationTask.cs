using System;
using System.IO;
using Microsoft.Build.Framework;
using WebApiProxy.Tasks.Infrastructure;
using WebApiProxy.Tasks.Models;

namespace WebApiProxy.Tasks
{
    public class ProxyGenerationTask : ITask
    {
        private Configuration config;

        [Output]
        public string Filename { get; set; }

        [Output]
        public string Root { get; set; }

        [Output]
        public string ProjectPath { get; set; }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        public bool Execute()
        {
            try
            {
                config = Configuration.Load(Root);

                if (config.GenerateOnBuild)
                {
                    var generator = new CSharpGenerator(config);
                    var source = generator.Generate();
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