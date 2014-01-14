using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProxy.Core.Models;


namespace WebApiProxy.Tasks.Templates
{
	public partial class CSharpProxyTemplate
	{
        public CSharpProxyTemplate(Configuration config)
		{
            
            this.Configuration = config;
		}

        public bool renderNamespaces { get; set; }
        public Configuration Configuration { get; set; }

    }
}
