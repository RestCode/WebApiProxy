using System.Collections.Generic;

namespace WebApiProxy.Core.Models
{
	public class ActionMethodDefinition
	{
		public string Type { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public IEnumerable<ParameterDefinition> UrlParameters { get; set; }

        public ParameterDefinition BodyParameter { get; set; }

        public string Description { get; set; }

        public string ReturnType { get; set; }

	}
}
