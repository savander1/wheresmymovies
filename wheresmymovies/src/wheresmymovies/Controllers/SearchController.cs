﻿using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using wheresmymovies.Data;
using System.Linq;
using System.Threading.Tasks;

namespace wheresmymovies.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public SearchController(IMovieRepository movieRepository) 
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public ObjectResult Get([FromQuery] MovieSearchParameters searchParams)
        {
            if (searchParams != null && searchParams.IsValid())
            {
                var movies = _movieRepository.Get(searchParams);
                if (movies == null || !movies.Any())
                {
                    return new HttpNotFoundObjectResult(new object());
                }
                return new ObjectResult(movies);
            }
            return new BadRequestObjectResult(new object());
        }

        [HttpGet("{id}")]
        public async Task<ObjectResult> Get(string id)
        {
            var movie =  await _movieRepository.Get(id);
            if (movie == null)
            {
                return new HttpNotFoundObjectResult(new Movie());
            }

            return new ObjectResult(movie);
        }
    }
}
