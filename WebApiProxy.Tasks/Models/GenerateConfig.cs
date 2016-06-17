using System.Collections.Generic;
using System.Linq;

namespace WebApiProxy.Tasks.Models
{
    public class GenerateConfig
    {
        public bool GenerateOnBuild { get; set; }
        public bool GenerateAsyncReturnTypes { get; set; }

        public IEnumerable<ServiceConfig> Services { get; set; }


        public class ServiceConfig
        {
            public ServiceConfig()
            {
                ClientSuffix = "Client";
            }

            public string ProxyEndpoint { get; set; }
            public string Namespace { get; set; }
            public string Name { get; set; }

            public string ClientSuffix { get; set; }

            public bool IncludeValidation { get; set; }
            public bool EnsureSuccess { get; set; }
        }


        public IEnumerable<Configuration> TransformToOldConfig()
        {
            return Services.Select(service => new Configuration
                                              {
                                                  GenerateOnBuild = GenerateOnBuild,
                                                  GenerateAsyncReturnTypes = GenerateAsyncReturnTypes,
                                                  Endpoint = service.ProxyEndpoint,
                                                  ClientSuffix = service.ClientSuffix,
                                                  Namespace = service.Namespace,
                                                  Name = service.Name
                                              });
        }
    }

}