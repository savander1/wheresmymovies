using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public class OmovieClient
    {
        private readonly string _omdbUrl;
        public OmovieClient(string omdbUrl)
        {
            _omdbUrl = omdbUrl;
        }

        public async Task<Movie> GetMovie(MovieSearchParameters parameters)
        {
            return await GetData(parameters).ContinueWith((antecedent) =>
            {
                try
                {
                    var data = antecedent.Result;
                    var oMovie = JsonConvert.DeserializeObject<Omovie>(data);
                    return new Movie(oMovie);
                }
                catch
                {
                    return null;
                }
            });
        }

        private async Task<string> GetData(MovieSearchParameters parameters)
        {
            var endPoint = GetEndpoint(parameters);
            using (var client = new HttpClient())
            {
                using (var stream = await client.GetStreamAsync(endPoint))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return await Task<string>.Factory.StartNew(() => reader.ReadToEnd());
                    }
                }           
            }
        }

        private Uri GetEndpoint(MovieSearchParameters parameters)
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
    }
}