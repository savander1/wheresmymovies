using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using wheresmymovies.Data;
using System.Linq;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/values
        [HttpGet]
        public IEnumerable<Movie> Get([FromQuery] MovieSearchParameters searchParams)
        {
            return _movieRepository.Search(searchParams);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Movie Get(string id)
        {
            var searchParams = new MovieSearchParameters { Id = id };
            return _movieRepository.Search(searchParams).SingleOrDefault();
        }
        
    }
}
