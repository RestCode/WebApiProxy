using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProxy.Core.Models
{
    public class ModelDefinition
    {
        public string Name { get; set; }

        public IEnumerable<ModelProperty> Properties { get; set; }

       
    }

    public class ModelProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
