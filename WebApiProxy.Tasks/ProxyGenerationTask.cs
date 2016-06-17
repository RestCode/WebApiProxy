using System.IO;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using WebApiProxy.Tasks.Infrastructure;
using WebApiProxy.Tasks.Models;

namespace WebApiProxy.Tasks
{
    public class ProxyGenerationTask : ITask
    {
        [Output]
        public string Root { get; set; }

        public IBuildEngine BuildEngine { get; set; }

        public ITaskHost HostObject { get; set; }

        public bool Execute()
        {
            var oldConfigFile = Path.Combine(Root, Configuration.ConfigFileName);
            if (File.Exists(oldConfigFile))
            {
                var config = Configuration.Load(Root);

                GenerateProxyFile(config);
            }
            else
            {
                var jsonConfigFile = Path.Combine(Root, Configuration.JsonConfigFileName);
                GenerateConfig jsonConfig;

                using (var sr = new StreamReader(jsonConfigFile))
                {
                    jsonConfig = JsonConvert.DeserializeObject<GenerateConfig>(sr.ReadToEnd());
                }

                var oldConfigs = jsonConfig.TransformToOldConfig();
                foreach (var config in oldConfigs)
                {
                    GenerateProxyFile(config);
                }
            }

            return true;
        }

        private void GenerateProxyFile(Configuration config)
        {
            if (!config.GenerateOnBuild)
            {
                return;
            }

            var csFilePath = Path.Combine(Root, config.Name) + ".cs";
            var generator = new CSharpGenerator(config);
            var source = generator.Generate();
            File.WriteAllText(csFilePath, source);
        }

    }
}