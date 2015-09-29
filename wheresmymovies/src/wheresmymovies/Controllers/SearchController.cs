using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using wheresmymovies.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace wheresmymovies.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return new Movie[] { new Movie() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Movie Get(string id)
        {
            return new Movie { };
        }
        
    }
}
