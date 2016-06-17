using System.IO;
using System.Xml.Serialization;

namespace WebApiProxy.Tasks.Models
{
    [XmlRoot("proxy")]
    public class Configuration
    {
        public const string ConfigFileName = "WebApiProxy.config";
        public const string JsonConfigFileName = "WebApiProxy.json";

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


        public static Configuration Load(string root)
        {
            var fileName = root + Configuration.ConfigFileName;

            if (!File.Exists(fileName))
            {
                throw new ConfigFileNotFoundException(fileName);
            }

            var serializer = new XmlSerializer(typeof(Configuration), new XmlRootAttribute("proxy"));
            var reader = new StreamReader(fileName);
            var config = (Configuration)serializer.Deserialize(reader);
            reader.Close();

            return config;

        }

    }

}