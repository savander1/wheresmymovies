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

            HasMany<Talent>(x => x.Director);
            HasMany<Talent>(x => x.Writer);
            HasMany<Talent>(x => x.Actor);

            Map(x => x.Plot);
            Map(x => x.Language);
            Map(x => x.Country);
            Map(x => x.Poster);


        }
    }
}
