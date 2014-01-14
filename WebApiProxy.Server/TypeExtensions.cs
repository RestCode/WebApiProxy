using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProxy
{
    public static class TypeExtensions
    {
        public static bool IsExcluded(this Type type)
        {
            return type.GetCustomAttributes(true).OfType<ExcludeProxy>().Any();
        }
    }
}
