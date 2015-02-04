using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheresMyMovies.Entities.Mappings
{
    public class GenreMapping : ClassMap<Genre>
    {
        public GenreMapping()
        {
            Id(x => x.Id);
            Map(x => x.Category);
            HasManyToMany(x => x.Movies)
                .Cascade.All()
                .Inverse()
                .Table("MovieGenre");
        }
    }
}
