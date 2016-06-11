using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApiProxy.Core.Models
{
	public class ControllerDefinition
	{
		public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ActionMethodDefinition> ActionMethods { get; set; }

		[JsonIgnore]
		public Type ControllerType { get; set; }
	}
}
