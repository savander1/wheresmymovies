using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using wheresmymovies.Entities;

namespace wheresmymovies.Data.Client
{
    public class AzureSearchClient : ISearchClient
    {
        private readonly string _searchUrl;
        private readonly string _apiKey;

    	public AzureSearchClient(AzureSearchConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _apiKey = configuration.ApiKey;
            _searchUrl = configuration.SearchEndpoint;
        }
        
        public async Task<HttpResponseMessage> GetAsync()
        {
            using (var client = new HttpClientEx())
            {
                return await client.GetAsync(_searchUrl);
            }
        }

        public async Task<HttpStatusCode> AddAsync(Movie movie)
        {
            using (var client = new HttpClientEx())
            {
                var azureMovie = new AzureMovie(movie, "mergeOrUpload");
                var response = await client.PostAsync(_searchUrl, await GetHttpContent(azureMovie));
                return response.StatusCode;
            }
        }
        
        public async Task<Movie> GetAsync(string id)
        {
            using (var client = new HttpClientEx())
            {
                var response = await client.GetAsync($"{_searchUrl}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<Movie>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<HttpStatusCode> DeleteAsync(string id)
        {
            using (var client = new HttpClientEx())
            {
                var movie = new Movie() { Id = id };
                var azureMovie = new AzureMovie(movie, "delete");
                var response = await client.PostAsync(_searchUrl, await GetHttpContent(azureMovie));
                return response.StatusCode;
            }
        }

        private async Task<HttpContent> GetHttpContent(AzureMovie movie)
        {
            var movieResponse = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(new AzureMovies(movie), Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore }));
            var content = new StringContent(movieResponse, Encoding.UTF8);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.Add("api-key", _apiKey);
       
            return content;
        }
    }
}
