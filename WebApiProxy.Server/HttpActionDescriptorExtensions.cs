using System.Linq;
using System.Web.Http.Controllers;

namespace WebApiProxy.Server
{
    public static class HttpActionDescriptorExtensions
    {
        public static bool IsExcluded(this HttpActionDescriptor descriptor)
        {
            return descriptor.GetCustomAttributes<ExcludeProxy>(true).Any();
        }
    }
}