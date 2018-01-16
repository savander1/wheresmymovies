using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using Newtonsoft.Json;

namespace wheresmymovies.Data.Client
{
    public class ImdbInfoClient : IInfoClient
    {
        private readonly IHttpClient _client;
        private const string BASE_URL = "http://www.imdb.com/xml/find";
        public ImdbInfoClient(IHttpClient client)
        {
            _client = client;
        }

        public async Task<IList<Movie>> SearchForMoviesAsync(SearchParameters parameters)
        {
            if (parameters.IsTitleSearch)
            {
                return await SearchForMoviesByTitleAsync(parameters.Title);
            }

            return await SearchForMoviesById(parameters.Id);
        }

        private async Task<IList<Movie>> SearchForMoviesById(string id)
        {
            throw new NotImplementedException();
        }

        private async Task<IList<Movie>> SearchForMoviesByTitleAsync(string title)
        {
            const string query = "?json=1&nr=1&tt=on&q=";
            var endpoint = $"{BASE_URL}{query}{title}";

            var response = await GetData(endpoint);

            return GetMovieList(response);
        }

        private List<Movie> GetMovieList(string response)
        {
            var dynamic = JsonConvert.DeserializeObject<dynamic>(response);

            var movies = new List<Movie>();

            foreach (var sub in dynamic.title_substring)
            {
                movies.Add(ToMovie(sub));
            }

            foreach(var movie in dynamic.title_popular)
            {
                movies.Insert(0, ToMovie(movie));
            }

            Movie ToMovie(dynamic d) => new Movie
            {
                Id = d.id,
                Title = d.title,
                Description = d.description,
                Rated = d.rated
            };

            return movies;
        }

        private async Task<string> GetData(string endpoint)
        {
            using (var response = await _client.GetAsync(endpoint))
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
