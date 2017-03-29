using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using wheresmymovies.Data.Client;

namespace wheresmymovies.Data
{
    public class MetaDataSearchRepositoryAysnc : IMetaDataSearchRepositoryAsync
    {
        private string _oMovieUrl;

        public MetaDataSearchRepositoryAysnc (string oMovieUrl)
        {
            if (string.IsNullOrEmpty(oMovieUrl)) throw new ArgumentNullException(nameof(oMovieUrl));
           _oMovieUrl = oMovieUrl;
        }

        public async Task<Movie> SearchAsync(SearchParameters searchParams)
        {
            var client = new OmovieClient(_oMovieUrl);
            return await client.GetMovie(searchParams.Decode());
        }
    }

}
