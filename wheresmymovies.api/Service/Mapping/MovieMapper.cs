using Microsoft.EntityFrameworkCore.Migrations.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace wheresmymovies.api.Service.Mapping
{
    public class MovieMapper : IMovieMapper
    {
        public entities.Movie ToEntity(Models.Movie model)
        {
            var movie = new entities.Movie
            {
                Id = model.Id,
                Title = model.Title,
                ThumbImgUrl = model.ThumbImgUrl,
                Description = model.Description,
                Runtime = model.Runtime,
                FullImgUrl = model.FullImgUrl
            };
            foreach (var actor in model.Actors)
            {
                movie.Actors.Add(new entities.MovieActor
                {
                    Actor = actor,
                    Id = movie.Id
                });
            }
            //TODO: etc

            return movie;
        }

        public Models.Movie ToModel(entities.Movie entity)
        {
            return entity == null ? null
                                  : new Models.Movie
                                  {
                                      Id = entity.Id,
                                      Title = entity.Title,
                                      ThumbImgUrl = entity.ThumbImgUrl,
                                      Description = entity.Description,
                                      Years = entity.Years.Select(x => x.Year).ToList(),
                                      Runtime = entity.Runtime,
                                      Genres = entity.Genres.Select(x => x.Genre).ToList(),
                                      Directors = entity.Directors.Select(x => x.Director).ToList(),
                                      Writers = entity.Writers.Select(x => x.Writer).ToList(),
                                      Actors = entity.Actors.Select(x => x.Actor).ToList(),
                                      FullImgUrl = entity.FullImgUrl
                                      //TODO: FormatLocations
                                  };
        }
    }
}
