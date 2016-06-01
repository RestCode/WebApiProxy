using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WebApiProxy.Core.Models;

namespace WebApiProxy.Tasks.Models
{
    [XmlRoot("proxy")]
    public class Configuration
    {
        public const string ConfigFileName = "WebApiProxy.config";
        public const string CacheFile = "WebApiProxy.generated.cache";

        private string _clientSuffix = "Client";
        private string _name = "MyWebApiProxy";
        private bool _generateOnBuild = false;
        private string _namespace = "WebApi.Proxies";
        private bool _generateAsyncReturnTypes = false;

        [XmlAttribute("generateOnBuild")]
        public bool GenerateOnBuild
        {
            get
            {
                return this._generateOnBuild;
            }
            set
            {
                this._generateOnBuild = value;
            }
        }

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

        

        [XmlAttribute("namespace")]
        public string Namespace
        {
            get
            {
                return this._namespace;
            }
            set
            {
                this._namespace = value;
            }
        }

        [XmlAttribute("name")]
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        [XmlAttribute("endpoint")]
        public string Endpoint { get; set; }

        [XmlAttribute("generateAsyncReturnTypes")]
        public bool GenerateAsyncReturnTypes
        {
            get
            {
                return _generateAsyncReturnTypes;
            }
            set
            {
                _generateAsyncReturnTypes = value;
            }
        }

        //[XmlAttribute("host")]
        //public string Host { get; set; }

        [XmlIgnore]
        public Metadata Metadata { get; set; }

        public static Configuration Load(string root)
        {
            var fileName = root + Configuration.ConfigFileName;

            if (!File.Exists(fileName))
            {
                throw new ConfigFileNotFoundException(fileName);
            }

            var xml = File.ReadAllText(fileName);
            var serializer = new XmlSerializer(typeof(Configuration), new XmlRootAttribute("proxy"));
            var reader = new StreamReader(fileName);
            var config = (Configuration)serializer.Deserialize(reader);
            reader.Close();

            //if (string.IsNullOrEmpty(config.Host))
            //{
            //    config.Host = config.Metadata.Host;
            //}

            return config;

        }

    }

}