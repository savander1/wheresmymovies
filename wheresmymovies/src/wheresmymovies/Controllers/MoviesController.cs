using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Configuration;
using wheresmymovies.Data;
using wheresmymovies.Entities;
using wheresmymovies.Models;

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

        // GET api/movies/
        [HttpGet]
        public Movie Get([FromQuery]MovieSearchParameters searchParameters)
        {
            var url = Startup.Configuration.Get<string>("Data:omovieUrl");
            var oMovieDatabaseReader = new OMovieDatabaseReader(url);
            Response.Headers.Add("content-type", new [] { "application/json"});
            return oMovieDatabaseReader.GetMovie(searchParameters);
        }

        // POST api/movies/
        [HttpPost]
        public void Post([FromBody]Movie value)
        {
            _movieRepository.Add(value);
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Movie value)
        {
            _movieRepository.Update(id, value);
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _movieRepository.Delete(id);
        }
    }
}
