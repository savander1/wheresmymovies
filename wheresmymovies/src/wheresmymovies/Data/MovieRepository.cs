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
        private readonly string _apiKey;
        public MovieRepository(string azureApiKey)
        {
            _apiKey = azureApiKey;
        }
        
        public async Task<bool> Add(Movie movie)
        {
            var azureClient = new AzureSearchClient(_apiKey);
            
            var result = await azureClient.Add(movie);
            if (!result)
            {
                var index = 1;
                
                while (index > RETRIES)
                {
                    result = await azureClient.Add(movie);
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
            
        }

        public async Task<ICollection<Movie>> Search(MovieSearchParameters searchParams)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(string id, Movie movie)
        {
            return Add(movie);
        }
    }
}
