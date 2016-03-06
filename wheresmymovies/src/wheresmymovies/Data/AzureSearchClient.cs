using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using wheresmymovies.Entities;

namespace wheresmymovies.Data
{
    public class AzureSearchClient
    {
        private readonly string _searchUrl;
        private readonly string _apiKey;
        private readonly ILogger _logger;

    	public AzureSearchClient(AzureSearchConfiguration configuration, ILogger logger)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            _apiKey = configuration.ApiKey;
            _searchUrl = configuration.SearchEndpoint;
            _logger = logger;
        }
        
        public async Task<HttpResponseMessage> Get()
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync(_searchUrl);
            }
        }

        public async Task<HttpStatusCode> Add(Movie movie)
        {
            using (var client = new HttpClient())
            {
                var azureMovie = new AzureMovie(movie, "mergeOrUpload");
                var response = await client.PostAsync(_searchUrl, await GetHttpContent(azureMovie));
                _logger.LogInformation(await response.Content.ReadAsStringAsync());
                return response.StatusCode;
            }
        }
        
        public async Task<Movie> Get(string id)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{_searchUrl}/{id}");

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
                var response = await client.PostAsync(_searchUrl, await GetHttpContent(azureMovie));
                return response.StatusCode;
            }
        }

        private async Task<HttpContent> GetHttpContent(AzureMovie movie)
        {
            var movieResponse = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(new AzureMovies(movie), Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore }));
            var content = new StringContent(movieResponse, Encoding.UTF8);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.Add("api-key", _searchUrl);
       
            return content;
        }
    }
}
