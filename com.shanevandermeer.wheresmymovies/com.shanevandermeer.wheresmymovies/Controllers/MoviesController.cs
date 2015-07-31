using com.shanevandermeer.wheresmymovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace com.shanevandermeer.wheresmymovies.Controllers
{
    public class MoviesController : ApiController
    {
        // GET: api/Movie
        public IEnumerable<Movie> Get()
        {
            return new Movie[] {  };
        }

        // GET: api/Movie/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Movie
        public void Post([FromBody]Movie value)
        {
        }

        // PUT: api/Movie/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Movie/5
        public void Delete(int id)
        {
        }
    }
}
