
using Microsoft.AspNet.Mvc;
using wheresmymovies.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace wheresmymovies.Controllers
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        // POST api/values
        [HttpPost]
        public void Post([FromBody]Movie value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Movie value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
        }
    }
}
