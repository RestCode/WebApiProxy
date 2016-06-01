using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProxy.Core.Models;

namespace WebApiProxy.Server.Templates
{
	/// <summary>
	/// A partial class implementation used to pass parameters to the proxy template.
	/// </summary>
	public partial class JsProxyTemplate
	{
		public JsProxyTemplate(Metadata metadata)
		{
            this.Metadata = metadata;
		}
		public Metadata Metadata { get; set; }
    }
}
