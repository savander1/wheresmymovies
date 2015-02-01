using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheresMyMovies.Entities.Mappings
{
    public class MovieMap : ClassMap<Movie>
    {
        public MovieMap()
        {
            Id(x => x.Id);
            Map(x => x.Title);
            Map(x => x.Year);
            Map(x => x.Rating);
            Map(x => x.Released);

            HasManyToMany<Talent>(x => x.Director);
            HasManyToMany<Talent>(x => x.Writer);
            HasManyToMany<Talent>(x => x.Actor);

            Map(x => x.Plot);
            Map(x => x.Language);
            Map(x => x.Country);
            Map(x => x.Poster);
            Map(x => x.MovieType);

        }
    }
}
