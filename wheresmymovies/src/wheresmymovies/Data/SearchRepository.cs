using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;

namespace wheresmymovies.Data
{
    public class SearchRepository: ISearchRepository 
    {
        private string _oMovieUrl;

        public SearchRepository(string oMovieUrl)
        {
            if (string.IsNullOrEmpty(oMovieUrl)) throw new ArgumentNullException(nameof(oMovieUrl));
           _oMovieUrl = oMovieUrl;
        }

        public async Task<Movie> Search(MovieSearchParameters searchParams)
        {
            var client = new OmovieClient(_oMovieUrl);
            return await client.GetMovie(searchParams.Decode());
        }
    }

}
