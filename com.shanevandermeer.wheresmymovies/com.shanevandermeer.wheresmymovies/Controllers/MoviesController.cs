using com.shanevandermeer.wheresmymovies.Models;
using com.shanevandermeer.wheresmymovies.Repository;
using System.Collections.Generic;
using System.Web.Http;

namespace com.shanevandermeer.wheresmymovies.Controllers
{
    public class MoviesController : ApiController
    {
        private IRepository<Movie> _repository;

        public MoviesController(IRepository<Movie> repository)
        {
            _repository = repository;
        }

        // GET: api/Movie
        public IEnumerable<Movie> Get()
        {
            return _repository.GetAll();
        }

        // GET: api/Movie/5
        public Movie Get(string id)
        {
            return _repository.Get(id);
        }

        // POST: api/Movie
        public void Post([FromBody]Movie value)
        {
            _repository.Insert(value);
        }

        // PUT: api/Movie/5
        public void Put(string id, [FromBody]Movie value)
        {
            _repository.Update(id, value);
        }

        // DELETE: api/Movie/5
        public void Delete(string id)
        {
            _repository.Delete(id);
        }
    }
}
