﻿using Microsoft.AspNet.Mvc;
using System;
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
        private readonly IMetaDataSearchRepository _searchRepository;

        public MoviesController(IMovieRepository movieRepository, IMetaDataSearchRepository searchRepository)
        {
            if (movieRepository == null) throw new ArgumentNullException(nameof(movieRepository));
            if (searchRepository == null) throw new ArgumentNullException(nameof(searchRepository));
            
            _movieRepository = movieRepository;
            _searchRepository = searchRepository;
        }

        // GET api/movies/
        [HttpGet]
        public async Task<ObjectResult> Get([FromQuery]MovieSearchParameters searchParameters)
        {
            if (searchParameters != null && searchParameters.IsValid())
            {
                var movie = await _searchRepository.Search(searchParameters);
                if (movie == null)
                {
                    return new HttpNotFoundObjectResult(new object());
                }

                return new HttpOkObjectResult(movie);
            }
            return new BadRequestObjectResult(new object());
           
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
        public async Task<HttpStatusCodeResult> Delete(string id)
        {
            var status = await _movieRepository.Delete(id);
            return new HttpStatusCodeResult(status);
        }
    }
}
