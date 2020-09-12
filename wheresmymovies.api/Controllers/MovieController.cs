using Microsoft.AspNetCore.Mvc;
using wheresmymovies.api.Models;
using wheresmymovies.api.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        
        // GET api/<MovieController>/5
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return _movieService.Get(id);
        }

        // POST api/<MovieController>
        [HttpPost]
        public Movie Post([FromBody] Movie value)
        {
            return _movieService.Save(value);
        }

        // PUT api/<MovieController>/5
        [HttpPut("{id}")]
        public Movie Put(int id, [FromBody] Movie value)
        {
            return _movieService.Update(id, value);
        }

        // DELETE api/<MovieController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _movieService.Delete(id);
        }
    }
}
