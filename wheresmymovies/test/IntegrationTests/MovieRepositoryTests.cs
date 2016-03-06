using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wheresmymovies.Data;
using wheresmymovies.Models;
using Xunit;

namespace test.IntegrationTests
{
    public class MovieRepositoryTests
    {
        //private readonly MovieRepository _movieRepository;
        //private readonly MetaDataSearchRepository _metaDataSearchRepository;
        /*
        * Commenting out until I can remove Static Dependancies from the AzureSearchClient.
        *
        public MovieRepositoryTests()
        {
            _movieRepository = new MovieRepository("4559E002B817ECFCE1BE91F698620F10", new LoggerFactory().AddConsole());
            _metaDataSearchRepository = new MetaDataSearchRepository("http://www.omdbapi.com/");
        }

        [Fact]
        public async void WorkFlow_HappyPath()
        {
            var searchParams = new MovieSearchParameters
            {
                Title = "Patton"
            };

            var movie = await _metaDataSearchRepository.Search(searchParams);
            Assert.NotNull(movie);

            var result = await _movieRepository.Add(movie);
            Assert.Equal(200, result);
        }

    */
    }
}
