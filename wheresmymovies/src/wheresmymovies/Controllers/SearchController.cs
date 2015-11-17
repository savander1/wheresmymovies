using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using wheresmymovies.Data;
using System.Linq;
using System.Threading.Tasks;

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
            return  _movieRepository.Get(searchParams);
        }

        [HttpGet("{id}")]
        public async Task<ObjectResult> Get(string id)
        {
            var movie =  await _movieRepository.Get(id);
            if (movie == null)
            {
                return new HttpNotFoundObjectResult(new Movie());
            }

            return new ObjectResult(movie);
        }
    }
}
