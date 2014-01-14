using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WebApiProxy.Core.Models
{
    [XmlRoot("proxy")]
    public class Configuration
    {
        public const string FileName = "WebApiProxy.config";
        public const string CacheFile = "WebApiProxySource.cache";

        private string _clientSuffix;

        [XmlAttribute("clientSuffix")]
        public string ClientSuffix
        {
            get
            {
                return _clientSuffix.DefaultIfEmpty("Client");
            }
            set
            {
                _clientSuffix = value;
            }
        }

        private string _namespace;

        [XmlAttribute("namespace")]
        public string Namespace
        {
            get
            {
                return _namespace.DefaultIfEmpty("ProxyApi.Proxies");
            }
            set
            {
                _namespace = value;
            }
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("endpoint")]
        public string Endpoint { get; set; }

        [XmlIgnore]
        public Metadata Metadata { get; set; }

        public static Configuration Load()
        {
            if (!File.Exists(Configuration.FileName))
                throw new ConfigFileNotFoundException();  

            var xml = File.ReadAllText(Configuration.FileName);
            var serializer = new XmlSerializer(typeof(Configuration),new XmlRootAttribute("proxy"));

            

            var reader = new StreamReader(Configuration.FileName);
            var config = (Configuration)serializer.Deserialize(reader);
            reader.Close();


            return config;

        }

    }

}
