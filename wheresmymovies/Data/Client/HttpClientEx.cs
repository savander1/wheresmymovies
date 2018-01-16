using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace wheresmymovies.Data.Client
{
    public interface IHttpClient : IDisposable
    {
        Task<Stream> GetStreamAsync(Uri endpoint);
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<HttpResponseMessage> PostAsync(string uri, HttpContent content);

    }

    public class HttpClientEx : HttpClient, IHttpClient
    {

    }
}
