using Microsoft.AspNet.Mvc;
using wheresmymovies.Models;
using wheresmymovies.Data;
using System;
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
            if (movieRepository == null) throw new ArgumentNullException(nameof(movieRepository));
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<ObjectResult> Get([FromQuery] MovieSearchParameters searchParams)
        {
            if (searchParams != null && searchParams.IsValid())
            {
                var movies = await _movieRepository.Get(searchParams);
                if (movies == null || !movies.Any())
                {
                    return new HttpNotFoundObjectResult(new object());
                }
                return new HttpOkObjectResult(movies);
            }
            return new BadRequestObjectResult(new object());
        }

        [HttpGet("{id}")]
        public async Task<ObjectResult> Get(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var movie = await _movieRepository.Get(id);
                if (movie == null)
                {
                    return new HttpNotFoundObjectResult(new object());
                }

                return new HttpOkObjectResult(movie);
            }
            return new BadRequestObjectResult(new object());
        }
    }
}
