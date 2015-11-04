using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using wheresmymovies.Data;
using System.Linq;

namespace wheresmymovies.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public SearchController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IEnumerable<Movie> Get([FromQuery] MovieSearchParameters searchParams)
        {
            return  _movieRepository.Search(searchParams).Result;
        }

        [HttpGet("{id}")]
        public Movie Get(string id)
        {
            var searchParams = new MovieSearchParameters { Id = id };
            return _movieRepository.Search(searchParams).Result.SingleOrDefault();
        }
    }
}
