using Microsoft.Framework.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
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
            if (string.IsNullOrEmpty(apiKey)) throw new ArgumentNullException(nameof(apiKey));
            ApiKey = apiKey;
        }

        public string ApiKey { get; private set; }
        
        public async Task<HttpResponseMessage> Get()
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync(SearchUrl);
            }
        }

        public async Task<HttpStatusCode> Add(Movie movie)
        {
            using (var client = new HttpClient())
            {
                var azureMovie = new AzureMovie(movie, "mergeOrUpload");
                var response = await client.PostAsync(SearchUrl, await GetHttpContent(azureMovie));
                return response.StatusCode;
            }
        }
        
        public async Task<Movie> Get(string id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(SearchUrl + "/" + id);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<Movie>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            using (var client = new HttpClient())
            {
                var movie = new Movie() { Id = id };
                var azureMovie = new AzureMovie(movie, "delete");
                var response = await client.PostAsync(SearchUrl, await GetHttpContent(azureMovie));
                return response.StatusCode;
            }
        }

        private async Task<HttpContent> GetHttpContent(AzureMovie movie)
        {
            var movieResponse = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(new AzureMovies(movie), Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver()}));
            var content = new StringContent(movieResponse, Encoding.UTF8);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.Add("api-key", ApiKey);
       
            return content;
        }
    }
}
