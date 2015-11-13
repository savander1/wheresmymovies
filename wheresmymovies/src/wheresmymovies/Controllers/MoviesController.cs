using Microsoft.AspNet.Mvc;
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
        private readonly ISearchRepository _searchRepository;

        public MoviesController(IMovieRepository movieRepository, ISearchRepository searchRepository)
        {
            _movieRepository = movieRepository;
            _searchRepository = searchRepository;
        }

        // GET api/movies/
        [HttpGet]
        public async Task<ObjectResult> Get([FromQuery]MovieSearchParameters searchParameters)
        {
            var movie = await _searchRepository.Search(searchParameters);
            if (movie == null)
            {
                return new HttpNotFoundObjectResult(new Movie());
            }

            return new ObjectResult(movie);
           
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
