using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WebApiProxy.Core.Models
{
    /// <summary>
    /// Represents the available configurations to a proxy.
    /// </summary>
    [XmlRoot("proxy")]
    public class Configuration
    {
        #region Constants
        /// <summary>
        /// The proxy configuration file.
        /// </summary>
        public const string FileName = "WebApiProxy.config";

        /// <summary>
        /// The proxy cache file.
        /// <remarks>
        /// Used when the api is not available due some connection problem.
        /// </remarks>
        /// </summary>
        public const string CacheFile = "WebApiProxySource.cache";
        #endregion

        #region Fields
        private string _clientSuffix;
        private string _namespace;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the client suffix.
        /// </summary>
        /// <value>
        /// The client suffix.
        /// </value>
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

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>
        /// The namespace.
        /// </value>
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

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the endpoint.
        /// </summary>
        /// <value>
        /// The endpoint.
        /// </value>
        [XmlAttribute("endpoint")]
        public string Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        /// <value>
        /// The metadata.
        /// </value>
        [XmlIgnore]
        public Metadata Metadata { get; set; }


        /// <summary>
        /// Gets or sets the remove from URL.
        /// </summary>
        /// <value>
        /// The remove from URL.
        /// </value>
        [XmlAttribute("removeFromUrl")]
        public string RemoveFromUrl { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the configuration from file.
        /// </summary>
        /// <returns>The configuration loaded.</returns>
        public static Configuration Load()
        {
            if (!File.Exists(Configuration.FileName))
                throw new ConfigFileNotFoundException();

            var xml = File.ReadAllText(Configuration.FileName);
            var serializer = new XmlSerializer(typeof(Configuration), new XmlRootAttribute("proxy"));

            var reader = new StreamReader(Configuration.FileName);
            var config = (Configuration)serializer.Deserialize(reader);
            reader.Close();

            return config;
        }
        #endregion
    }
}
