using ProxyApi.Proxies.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ProxyApi.Proxies.Models;
using System.Threading;
using System.Net.Http.Formatting;

namespace WebApiProxy.Sample.Client
{

    //public static class MyClass
    //{
    //    public static Task<T> ContentAsAsync<T>(this HttpResponseMessage message)
    //    {
    //        return message.Content.ReadAsAsync<T>();
    //    }

    //    public static Task<T> ContentAsAsync<T>(this HttpResponseMessage message, CancellationToken cancellationToken)
    //    {
    //        return message.Content.ReadAsAsync<T>(cancellationToken);
    //    }

    //    public static Task<T> ContentAsAsync<T>(this HttpResponseMessage message, IEnumerable<MediaTypeFormatter> formatters)
    //    {
    //        return message.Content.ReadAsAsync<T>(formatters);
    //    }

    //    public static Task<T> ContentAsAsync<T>(this HttpResponseMessage message, IEnumerable<MediaTypeFormatter> formatters, CancellationToken cancellationToken)
    //    {
    //        return message.Content.ReadAsAsync<T>(formatters,cancellationToken);
    //    }

    //    public static Task<T> ContentAsAsync<T>(this HttpResponseMessage message, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
    //    {
    //        return message.Content.ReadAsAsync<T>(formatters, formatterLogger);
    //    }

    //    public static Task<T> ContentAsAsync<T>(this HttpResponseMessage message, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
    //    {
    //        return message.Content.ReadAsAsync<T>(formatters, formatterLogger,cancellationToken);
    //    }
    //}
    class Program
    {
        static void Main(string[] args)
        {
            Get();
            Console.ReadKey();
        }

        static async void Get()
        {
            
            using (var client = new ApiClient())
            {
                var response = await client.GetAsync( );
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsAsync<object>();
            }
	    
        }
	    





            
          
	    
	    
	    

            
	    
	    
        }
    }
}
