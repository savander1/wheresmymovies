using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wheresmymovies.Data;
using wheresmymovies.Models;

namespace wheresmymovies.Controllers
{
    public class MovieModule : NancyModule
    {
        public MovieModule(IMovieRepository movieRepository, IMetaDataSearchRepository metaDataRepository) : base("/movie")
        {
            if (movieRepository == null) throw new ArgumentNullException(nameof(movieRepository));
            if (metaDataRepository == null) throw new ArgumentNullException(nameof(metaDataRepository));

            Get("/", async (x, ctx) =>
            {
                MovieSearchParameters searchParameters = this.BindTo((MovieSearchParameters)x);
                if (!searchParameters.IsValid())
                    return HttpStatusCode.BadRequest;

                var movie = await metaDataRepository.Search(searchParameters);
                if (movie == null)
                {
                    return HttpStatusCode.NotFound;
                }

                return movie;
            });

            //Post("/", movie =>
            //{
            //    movieRepository.Add(movie);
            //    return HttpStatusCode.OK;
            //});

        }
    }
}
