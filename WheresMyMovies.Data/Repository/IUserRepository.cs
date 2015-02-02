
using System.Linq;
using WheresMyMovies.Entities;

namespace WheresMyMovies.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUser(string emailOrUsername);
        bool UserIsInRole(string emailOrUsername, string roleName);
        IQueryable<User> SearchByPartialUsername(string partialEmailOrUsername);
    }
}
