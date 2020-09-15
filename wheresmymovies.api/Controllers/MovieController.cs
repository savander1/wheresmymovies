using Microsoft.AspNetCore.Mvc;
using wheresmymovies.api.Models;
using wheresmymovies.api.Service;

namespace wheresmymovies.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService) 
        {
            _movieService = movieService;
        }
     
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return _movieService.Get(id);
        }

        [HttpPost]
        public Movie Post([FromBody] Movie value)
        {
            return _movieService.Save(value);
        }

        [HttpPut("{id}")]
        public Movie Put(int id, [FromBody] Movie value)
        {
            return _movieService.Update(id, value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _movieService.Delete(id);
        }
    }
}
