using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using wheresmymovies.Data.Client;

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
            return  await _azureClient.GetAsync(id);
        }

        public Task<List<Movie>> GetAsync(SearchFilters searchFilters)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> SearchAsync(SearchParameters searchParams)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(string id, Movie movie)
        {
            return await AddAsync(movie);
        }
    }
}
