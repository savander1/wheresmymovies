using System;
using System.Collections.Generic;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly string _apiKey;
        public MovieRepository(string azureApiKey)
        {
            _apiKey = azureApiKey;
        }
        
        public bool Add(Movie movie)
        {
            var azureClient = new AzureSearchClient(_apiKey);
            
            azureClient.Add(movie).ContinueWith((anticedent) => {
                var result = anticedent.Result;
                
            });
        }

        public bool Delete(string movie)
        {
            throw new NotImplementedException();
        }

        public ICollection<Movie> Search(MovieSearchParameters searchParams)
        {
            throw new NotImplementedException();
        }

        public bool Update(string id, Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
