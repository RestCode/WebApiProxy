using System.Collections.Generic;

namespace WebApiProxy.Core.Models
{
	public class ControllerDefinition
	{
		public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ActionMethodDefinition> ActionMethods { get; set; }
 
    }
}
