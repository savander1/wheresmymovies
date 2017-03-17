using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public class MovieRepository : IMovieRepository
    {
        private const int RETRIES = 9;
        private readonly AzureSearchClient _azureClient;
        private readonly ILogger _logger;

        public MovieRepository(AzureSearchConfiguration configuration, ILoggerFactory loggerfactory)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (loggerfactory == null) throw new ArgumentNullException(nameof(loggerfactory));

            var azureClientLogger = loggerfactory.CreateLogger<AzureSearchClient>();
            _azureClient = new AzureSearchClient(configuration, azureClientLogger);
            _logger = loggerfactory.CreateLogger<MovieRepository>();
        }
        
        public async Task<int> Add(Movie movie)
        {
            var result = await _azureClient.Add(movie);
            if (result != System.Net.HttpStatusCode.OK)
            {
                var index = 1;
                
                while (index <= RETRIES)
                {
                    result = await _azureClient.Add(movie);
                    _logger.LogInformation(result.ToString());
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

        public async Task<int> Delete(string movieId)
        {
            var result = await _azureClient.Delete(movieId);
            if (result != System.Net.HttpStatusCode.OK)
            {
                var index = 1;

                while (index <= RETRIES)
                {
                    result = await _azureClient.Delete(movieId);
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

        public async Task<Movie> Get(string id)
        {
            return  await _azureClient.Get(id);
        }

        public Task<ICollection<Movie>> Get(SearchParameters searchParams)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(string id, Movie movie)
        {
            return await Add(movie);
        }
    }
}
