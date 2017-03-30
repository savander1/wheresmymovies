using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data.Client
{
    public class OmovieClient
    {
        private readonly string _omdbUrl;
        private readonly IHttpClient _httpClient;
        private bool _disposedValue = false;

        public OmovieClient(string omdbUrl, IHttpClient client)
        {
            _omdbUrl = omdbUrl;
            _httpClient = client;
        }

        public async Task<Movie> GetMovie(SearchParameters parameters)
        {
            var data = await GetData(parameters);
            var movie = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Omovie>(data));
            return new Movie(movie);
        }

        private async Task<string> GetData(SearchParameters parameters)
        {
            var endPoint = GetEndpoint(parameters);
            using (var stream = await _httpClient.GetStreamAsync(endPoint))
            {
                using (var reader = new StreamReader(stream))
                {
                    return await Task<string>.Factory.StartNew(() => reader.ReadToEnd());
                }
            }
        }

        private Uri GetEndpoint(SearchParameters parameters)
        {
            var builder = new StringBuilder(_omdbUrl);
            if (!string.IsNullOrEmpty(parameters.Id))
            {
                builder.Append("?i=" + parameters.Id.Trim());
            }
            else
            {
                builder.Append("?t=" + parameters.Title.Trim());
            }
            builder.Append("&plot=full&r=json");
            return new Uri(builder.ToString());
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _httpClient.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}