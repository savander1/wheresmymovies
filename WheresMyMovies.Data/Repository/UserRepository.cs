using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WheresMyMovies.Entities;

namespace WheresMyMovies.Data.Repository
{
    class UserRepository : IUserRepository 
    {
        public IQueryable<User> Get()
        {
            return new List<User>().AsQueryable();
        }

        public User GetUser(string emailOrUsername)
        {
            return Get().Where(x => x.UserName.Equals(emailOrUsername, StringComparison.InvariantCultureIgnoreCase)
                                    || x.Email.Equals(emailOrUsername, StringComparison.InvariantCultureIgnoreCase))
                        .FirstOrDefault();

        }

        public bool UserIsInRole(string emailOrUsername, string roleName)
        {
            var user = GetUser(emailOrUsername);

            return user != null && user.UserRole.ToString().Equals(roleName, StringComparison.InvariantCultureIgnoreCase);
        }

        public IQueryable<User> SearchByPartialUsername(string partialEmailOrUsername)
        {
            var expression = new Regex(string.Format("^.*{0}.*$", partialEmailOrUsername));
            return Get().Where(x => expression.IsMatch(x.Email)
                                 || expression.IsMatch(x.UserName));
        }
    }
}
