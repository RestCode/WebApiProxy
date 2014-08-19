using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApiProxy.Core.Models
{
	public class ParameterDefinition 
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public bool IsOptional { get; set; }

        public object DefaultValue { get; set; }
    }
}
