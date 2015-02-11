using System.Web.Security;
using System.Configuration.Provider;
using System.Collections.Specialized;
using System;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using WheresMyMovies.Data.Repository;
using WheresMyMovies.Entities;
using System.Collections.Generic;

namespace WheresMyMovies.Core
{
    public class AppMembershipProvider : MembershipProvider 
    {
        private readonly IUserRepository _userRepository;

        public AppMembershipProvider(IUserRepository userRepository) : base()
        {
            if (userRepository == null) throw new ArgumentNullException("userRepository");

            _userRepository = userRepository;
        }

        public override string ApplicationName { get; set; }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = _userRepository.GetUser(username);

            if (user != null && user.Password.Equals(oldPassword))
            {
                user.Password = newPassword;
                _userRepository.Save(user);
                return true;
            }

            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new InvalidOperationException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var args = new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && !string.IsNullOrEmpty(GetUserNameByEmail(email)))
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            var existing = GetUser(username, false);

            if (existing != null)
            {
                status = MembershipCreateStatus.DuplicateUserName;
                return null;
            }

            var user = new User
            {
                Email = email,
                Password = password,
                Token = TokenGenerator.GenerateToken(),
                UserName = username,
                UserRole = Role.Standard
            };

            try
            {
                _userRepository.Save(user);
            }
            catch (Exception)
            {
                status = MembershipCreateStatus.ProviderError;
                return null;
            }

            status = MembershipCreateStatus.Success;
            return UserToMembershipUser(user);
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new InvalidOperationException();
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection(); 
            var users = _userRepository.Get().Where(x => x.Email.Equals(emailToMatch, StringComparison.InvariantCultureIgnoreCase));
            totalRecords = users.Count();

            var usersToSkip = pageIndex * pageSize;

            var userMembers = users.Skip(usersToSkip).Take(pageSize).ToList();

            if (users.Any())
            {
                foreach (var user in users)
                {
                    collection.Add(UserToMembershipUser(user));
                }
            }
            return collection;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection();
            var users = _userRepository.Get().Where(x => x.UserName.Equals(usernameToMatch, StringComparison.InvariantCultureIgnoreCase));
            totalRecords = users.Count();

            var usersToSkip = pageIndex * pageSize;

            var userMembers = users.Skip(usersToSkip).Take(pageSize).ToList();

            if (users.Any())
            {
                foreach (var user in users)
                {
                    collection.Add(UserToMembershipUser(user));
                }
            }
            return collection;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var collection = new MembershipUserCollection();
            var users = _userRepository.Get();
            totalRecords = users.Count();

            var usersToSkip = pageIndex * pageSize;

            var userMembers = users.Skip(usersToSkip).Take(pageSize).ToList();

            if (users.Any())
            {
                foreach (var user in users)
                {
                    collection.Add(UserToMembershipUser(user));
                }
            }
            return collection;
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new InvalidOperationException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new InvalidOperationException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = _userRepository.GetUser(username);

            return UserToMembershipUser(user);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var user = _userRepository.Get().SingleOrDefault(x => x.Token.Equals(providerUserKey));

            return UserToMembershipUser(user);
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 5; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 1; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 8; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 1; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new InvalidOperationException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var user = _userRepository.GetUser(username);

            return user != null && user.Password.Equals(password);
        }

        private MembershipUser UserToMembershipUser(User user)
        {
            if (user == null) return null;

            return new MembershipUser(providerName: this.Name,
                                      name: null,
                                      providerUserKey: user.Token,
                                      email: user.Email,
                                      passwordQuestion: string.Empty,
                                      comment: string.Empty,
                                      isApproved: true,
                                      isLockedOut: false,
                                      creationDate: DateTime.UtcNow,
                                      lastLoginDate: DateTime.UtcNow,
                                      lastActivityDate: DateTime.UtcNow,
                                      lastPasswordChangedDate: DateTime.UtcNow,
                                      lastLockoutDate: DateTime.UtcNow);
                                                    
        }

        private static string _machineValidationKey;
        private string machineValidationKey
        {
            get
            {
                return _machineValidationKey ?? (_machineValidationKey = GetMachineValidationKey());
                
            }
        }

        private string EncodePassword(string password)
        {
            var key = new List<byte>();
            for (int i = 0; i < machineValidationKey.Length / 2; i++) 
            { 
                key.Add(Convert.ToByte(machineValidationKey.Substring(i * 2, 2), 16));
            }
            
             
            HMACSHA1 hash = new HMACSHA1(key.ToArray());
            var pwBytes = Encoding.Unicode.GetBytes(password);
            var hashedBytes = hash.ComputeHash(pwBytes);

            return Convert.ToBase64String(hashedBytes);

        }

        private static string GetMachineValidationKey()
        {
            var cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            if (cfg == null)
            {
                throw new ArgumentNullException();
            }
            var machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");
            if (machineKey == null)
            {
                throw new ArgumentNullException();
            }
            return machineKey.ValidationKey;
        }
    }
}
