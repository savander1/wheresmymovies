using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Configuration;
using System.Net;
using System.Threading.Tasks;
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
        public async Task<HttpStatusCodeResult> Post(Movie value)
        {
            var status = await _movieRepository.Add(value);
            return new HttpStatusCodeResult(status);
        }

        // PUT api/movies/5
        [HttpPut("{id}")]
        public async Task<HttpStatusCodeResult> Put(string id, Movie value)
        {
            var status = await _movieRepository.Update(id, value);
            return new HttpStatusCodeResult(status);
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public HttpStatusCodeResult Delete(string id)
        {
            _movieRepository.Delete(id);
            return new HttpOkResult();
        }
    }
}
