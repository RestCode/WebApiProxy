using ProxyApi.Proxies.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ProxyApi.Proxies.Models;

namespace WebApiProxy.Sample.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Get();
            Console.ReadKey();
        }

        static async void Get()
        {
            using (var client = new PeopleClient())
            {

                client.GetAsync("sddd", 22, "aaa");

                var response = await client.PostAsync(new Person { Id = 2, FirstName = "SSSS" });

                //var people = await response.Content.ReadAsAsync<Person[]>();
            }
        }
    }
}
