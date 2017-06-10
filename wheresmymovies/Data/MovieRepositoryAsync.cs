using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using wheresmymovies.Data.Client;
using Newtonsoft.Json;

namespace wheresmymovies.Data
{
    public class MovieRepositoryAsync : IMovieRepositoryAsync
    {
        private const int RETRIES = 9;
        private readonly ISearchClient _azureClient;
        private readonly IInfoClient _infoClient;

        public MovieRepositoryAsync(ISearchClient azureClient, IInfoClient infoClient)
        {
            if (azureClient == null) throw new ArgumentNullException(nameof(azureClient));
            if (infoClient == null) throw new ArgumentNullException(nameof(infoClient));

            _azureClient = azureClient;
            _infoClient = infoClient;
        }
        
        public async Task<int> AddAsync(Movie movie)
        {
            if (movie == null) throw new ArgumentNullException(nameof(movie));

            var result = await _azureClient.AddAsync(movie);
            if (result != System.Net.HttpStatusCode.OK)
            {
                var index = 1;
                
                while (index <= RETRIES)
                {
                    result = await _azureClient.AddAsync(movie);
                    if (result == System.Net.HttpStatusCode.OK)
                    {
                        break;
                    }
                    index++;
                    Thread.Sleep(TimeSpan.FromSeconds(1d));
                }
            }
            
            return (int)result;
        }

        public async Task<int> DeleteAsync(string movieId)
        {
            if (string.IsNullOrWhiteSpace(movieId)) throw new ArgumentNullException(nameof(movieId));

            var result = await _azureClient.DeleteAsync(movieId);
            if (result != System.Net.HttpStatusCode.OK)
            {
                var index = 1;

                while (index <= RETRIES)
                {
                    result = await _azureClient.DeleteAsync(movieId);
                    if (result == System.Net.HttpStatusCode.OK)
                    {
                        break;
                    }
                    index++;
                    Thread.Sleep(TimeSpan.FromSeconds(1d));
                }
            }

            return (int)result;
        }

        public async Task<Movie> GetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

            return  await _azureClient.GetAsync(id);
        }

        public async Task<List<Movie>> GetAsync(SearchFilters searchFilters)
        {
            var response = await _azureClient.GetAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }
            var content = await response.Content.ReadAsStringAsync();
            return await Task.Factory.StartNew<List<Movie>>(() =>
            {
                return JsonConvert.DeserializeObject<List<Movie>>(content);
            });
        }

        public Task<Movie> SearchAsync(SearchParameters searchParams)
        {
            if (searchParams == null) throw new ArgumentNullException(nameof(searchParams));
            if (!searchParams.IsValid()) throw new ArgumentOutOfRangeException(nameof(searchParams), searchParams, "Invalid");

            return _infoClient.GetMovieAsync(searchParams);
        }

        public async Task<int> UpdateAsync(string id, Movie movie)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

            return await AddAsync(movie);
        }
    }
}
