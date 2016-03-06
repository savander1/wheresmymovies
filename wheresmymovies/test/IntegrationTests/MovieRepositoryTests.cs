using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wheresmymovies;
using wheresmymovies.Data;
using wheresmymovies.Models;
using Xunit;

namespace test.IntegrationTests
{
    public class MovieRepositoryTests
    {
        private readonly MovieRepository _movieRepository;
        private readonly MetaDataSearchRepository _metaDataSearchRepository;

        public MovieRepositoryTests()
        {
            var searchConfiguration = new AzureSearchConfiguration("4559E002B817ECFCE1BE91F698620F10", "https://wheresmymoves.search.windows.net/indexes/movies/docs/index?api-version=2015-02-28");
            _movieRepository = new MovieRepository(searchConfiguration, new LoggerFactory().AddConsole());
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
    }
}
