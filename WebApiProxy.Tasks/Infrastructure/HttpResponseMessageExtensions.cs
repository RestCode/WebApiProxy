using System.Net.Http;
using System.Threading.Tasks;
public static class HttpResponseMessageExtensions
{
    public static Task<T> ReadContentAsAsync<T>(this HttpResponseMessage response)
    {
        return response.Content.ReadAsAsync<T>();
    }
}

