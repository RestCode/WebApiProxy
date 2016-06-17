using System;
using System.Linq;

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
