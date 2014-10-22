using System.Collections.Generic;
using System.Diagnostics;
using HelperSharp;

namespace WebApiProxy.Core.Models
{
    [DebuggerDisplay("{Name}")]
    public class ModelDefinition
    {
        #region Constructors
        public ModelDefinition()
        {
            GenericArgumentsMap = new Dictionary<string, string>();
            Constants = new List<ConstantDefinition>();
            Properties = new List<ModelProperty>();
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public IDictionary<string, string> GenericArgumentsMap { get; set; }

        public IEnumerable<ConstantDefinition> Constants { get; set; }
        public IEnumerable<ModelProperty> Properties { get; set; }
        #endregion

        #region Methods
        public string AddGenericArgument(string argument)
        {
            var result = "T{0}".With(GenericArgumentsMap.Count + 1);
            GenericArgumentsMap.Add(argument, result);

            return result;
        }

        public string GetGenericArgument(string argument)
        {
            return GenericArgumentsMap[argument];
        }

        public bool IsGenericArgument(string argument)
        {
            return GenericArgumentsMap.ContainsKey(argument);
        }
        #endregion
    }
}
