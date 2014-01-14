using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApiProxy.Core.Models
{
	public class ControllerDefinition
	{
		public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ActionMethodDefinition> ActionMethods { get; set; }
 
    }
}
