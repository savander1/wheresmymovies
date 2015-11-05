using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public class MovieRepository : IMovieRepository
    {
        private const int RETRIES = 10;
        private readonly AzureSearchClient _azureClient;

        public MovieRepository(string azureApiKey)
        {
            if (string.IsNullOrEmpty(azureApiKey)) throw new ArgumentNullException(nameof(azureApiKey));
            _azureClient = new AzureSearchClient(azureApiKey);
        }
        
        public async Task<bool> Add(Movie movie)
        {
            var result = await _azureClient.Add(movie);
            if (!result)
            {
                var index = 1;
                
                while (index > RETRIES)
                {
                    result = await _azureClient.Add(movie);
                    if (result)
                    {
                        break;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(1d));
                }
            }
            
            return result;
        }

        public async Task<bool> Delete(string movieId)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Movie>> Search(MovieSearchParameters searchParams)
        {
            var response = await _azureClient.Get();
            
        }

        public async Task<bool> Update(string id, Movie movie)
        {
            return await Add(movie);
        }
    }
}
