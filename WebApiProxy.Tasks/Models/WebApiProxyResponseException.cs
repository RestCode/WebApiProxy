using System;
using System.Net.Http;

namespace WebApiProxy.Tasks.Models
{
    public class WebApiProxyResponseException : Exception
    {

        public HttpResponseMessage Response { get; private set; }


        public WebApiProxyResponseException(HttpResponseMessage response) : base("A " + response.StatusCode + " (" + (int)response.StatusCode + ") http exception occured. See response.")
        {
            Response = response;
        }
    }
}