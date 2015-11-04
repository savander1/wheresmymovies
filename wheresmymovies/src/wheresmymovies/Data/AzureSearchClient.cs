using Microsoft.Framework.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using wheresmymovies.Entities;

namespace wheresmymovies.Data
{
    public class AzureSearchClient
    {
        private static readonly string SearchUrl;

        static AzureSearchClient()
        {
            var urlFormatter = Startup.Configuration.Get<string>("Data:search:searchUrl");
            var index = Startup.Configuration.Get<string>("Data:search:indexName");
            var apiVersion = Startup.Configuration.Get<string>("Data:search:apiVersion");
            SearchUrl = string.Format(urlFormatter, index, apiVersion);
        }

    	public AzureSearchClient(string apiKey)
        {
            ApiKey = apiKey;
        }

        public string ApiKey { get; private set; }

        public async Task<bool> Add(Movie movie)
        {
            using (var client = new HttpClient())
            {
                var azureMovie = new AzureMovie(movie, "mergeOrUpload")
                var response = await client.PostAsync(SearchUrl, await GetHttpContent(azureMovie));
                return response.IsSuccessStatusCode;
            }
        }

        private async Task<HttpContent> GetHttpContent(AzureMovie movie)
        {
            var content = new MultipartContent();
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.Add("api-key", ApiKey);
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                var movieResponse = await Task.Factory.StartNew( () => JsonConvert.SerializeObject(movie, Formatting.Indented) );
                var responseBytes = Encoding.UTF8.GetBytes(movieResponse);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                stream.Seek(0, SeekOrigin.Begin);
                await content.CopyToAsync(stream);
            }
                
            return content;
        }
    }
}
