using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheresMyMovies.Entities.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.Email);
            Map(x => x.UserName);
            Map(x => x.Password);
            Map(x => x.UserRole);
        } 
    }
}
