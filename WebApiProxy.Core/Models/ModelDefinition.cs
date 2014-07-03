using System.Collections.Generic;

namespace WebApiProxy.Core.Models
{
    public class ModelDefinition
    {
        public string Name { get; set; }

        public IEnumerable<ConstantDefinition> Constants { get; set; }
        public IEnumerable<ModelProperty> Properties { get; set; }


    }

    public class ModelProperty
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
