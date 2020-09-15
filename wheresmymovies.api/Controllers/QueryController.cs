using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using wheresmymovies.api.Models;
using wheresmymovies.api.Service;

namespace wheresmymovies.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public QueryController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public IEnumerable<Movie> Post([FromBody] Movie value)
        {
            return _movieService.Find(value);
        }
    }
}
