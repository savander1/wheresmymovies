using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheresMyMovies.Entities.Mappings
{
    public class TalentMap : ClassMap<Talent>
    {
        public TalentMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.LastName);
            
            DiscriminateSubClassesOnColumn("Type");

            HasManyToMany<Movie>(x => x.Movies)
                .Cascade.All()
                .Inverse()
                .Table("MovieTalent");
        }
    }

    public class ActorMap : SubclassMap<Talent>
    {
        public ActorMap()
        {
            DiscriminatorValue(TalentType.Actor);
        }
    }

    public class DirectorMap : SubclassMap<Talent>
    {
        public DirectorMap()
        {
            DiscriminatorValue(TalentType.Director);
        }
    }

    public class WriterMap : SubclassMap<Talent>
    {
        public WriterMap()
        {
            DiscriminatorValue(TalentType.Writer);
        }
    }
}
