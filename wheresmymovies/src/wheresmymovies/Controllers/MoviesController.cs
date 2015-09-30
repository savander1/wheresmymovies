using Microsoft.AspNet.Mvc;
using wheresmymovies.Data;
using wheresmymovies.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace wheresmymovies.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Movie value)
        {
            _movieRepository.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Movie value)
        {
            _movieRepository.Update(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _movieRepository.Delete(id);
        }
    }
}
