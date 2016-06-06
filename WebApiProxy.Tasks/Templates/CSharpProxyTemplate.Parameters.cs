using WebApiProxy.Core.Models;
using WebApiProxy.Tasks.Models;


namespace WebApiProxy.Tasks.Templates
{
	public partial class CSharpProxyTemplate
	{
	    public Metadata MetaData { get; set; }
        public Configuration Configuration { get; set; }

        public CSharpProxyTemplate(Configuration config, Metadata metaData)
        {
            this.Configuration = config;
            this.MetaData = metaData;
        }
	}
}
