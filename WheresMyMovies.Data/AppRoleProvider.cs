using System;
using System.Linq;
using System.Web.Security;
using WheresMyMovies.Data.Repository;
using WheresMyMovies.Entities;

namespace WheresMyMovies.Data
{
    public class AppRoleProvider : RoleProvider
    {
        private readonly IUserRepository _userRepository;

        public AppRoleProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return _userRepository.UserIsInRole(username, roleName); ;
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = _userRepository.GetUser(username);

            if (user != null)
            {
                var userRole = user.UserRole;

                return user.UserRole == Role.Admin ? new[] { Role.Admin.ToString(), Role.Standard.ToString() }
                                                   : new[] { Role.Standard.ToString() };
            }

            return new string[] {};
        }

        public override void CreateRole(string roleName)
        {
            throw new InvalidOperationException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new InvalidOperationException();
        }

        public override bool RoleExists(string roleName)
        {
            return roleName.Equals(Role.Admin.ToString(), StringComparison.InvariantCultureIgnoreCase) || roleName.Equals(Role.Standard.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new InvalidOperationException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new InvalidOperationException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return _userRepository.Get().Where(x => x.UserRole.ToString().Equals(roleName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.UserName).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return new[] { Role.Admin.ToString(), Role.Standard.ToString() };
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return _userRepository.SearchByPartialUsername(usernameToMatch).Where(x => x.UserRole.ToString().Equals(roleName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.UserName).ToArray();
        }

        public override string ApplicationName { get; set; }
    }
}
