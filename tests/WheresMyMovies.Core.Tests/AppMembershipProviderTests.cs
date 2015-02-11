using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WheresMyMovies.Data.Repository;
using WheresMyMovies.Entities;
using System.Web.Security;

namespace WheresMyMovies.Core.Tests
{
    [TestClass]
    public class AppMembershipProviderTests
    {
        private Mock<IUserRepository> _userRepo;
        private AppMembershipProvider _provider;

        [TestInitialize]
        public void Setup()
        {
            _userRepo = new Mock<IUserRepository>();
            _provider = new AppMembershipProvider(_userRepo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctr_RepositoryNull_ThrowsException()
        {
            // arrange

            // act
            _provider = new AppMembershipProvider(null);

            // assert

        }

        [TestMethod]
        public void ValidateUser_UserValid_ReturnsTrue()
        {
            // arrange
            const string password = "Buzz";
            const string userName = "Chuck";

            _userRepo.Setup(x => x.GetUser(userName)).Returns(new User { UserName = userName, Password = "Buzz" });

            // act
            var result = _provider.ValidateUser(userName, password);

            // assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateUser_UserInValid_ReturnsFalse()
        {
            // arrange
            const string password = "Buzz";
            const string userName = "Chuck";

            _userRepo.Setup(x => x.GetUser(userName)).Returns(new User { UserName = userName, Password = "buzz" });

            // act
            var result = _provider.ValidateUser(userName, password);

            // assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ChangePassword_UserValid_PasswordChanged()
        {
            // arrange
            const string newPassword = "fizz";
            const string oldPassword = "Buzz";
            const string userName = "Chuck";

            _userRepo.Setup(x => x.GetUser(userName)).Returns(new User { UserName = userName, Password = oldPassword });

            // act
            var result = _provider.ChangePassword(userName, oldPassword, newPassword);

            // assert
            Assert.IsTrue(result);
            _userRepo.Verify(x => x.Save(It.IsAny<User>()), Times.Once);
        }

        [TestMethod]
        public void ChangePassword_UserNotValid_PassworNotChanged()
        {
            // arrange
            const string newPassword = "fizz";
            const string oldPassword = "buzz";
            const string userName = "Chuck";

            _userRepo.Setup(x => x.GetUser(userName)).Returns(new User { UserName = userName, Password = "Buzz" });

            // act
            var result = _provider.ChangePassword(userName, oldPassword, newPassword);

            // assert
            Assert.IsFalse(result);
            _userRepo.Verify(x => x.Save(It.IsAny<User>()), Times.Never);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ChangePasswordQuestionAndAnswer_ValidCall_ThrowsException()
        {
            // arrange

            // act
            _provider.ChangePasswordQuestionAndAnswer("username", "password", "question", "answer");

            // assert
        }

        [TestMethod]
        public void CreateUser_UserIsValid_StatusAndUserMatchExpected()
        {
            // arrange
            const MembershipCreateStatus expectedStatus = MembershipCreateStatus.Success;
            MembershipCreateStatus actualStatus;
            var user = CreateUser(expectedStatus);

            // act
            var result = _provider.CreateUser(user.UserName, user.Password, user.Email, "", "", true, null, out actualStatus);

            // assert
            Assert.AreEqual(expectedStatus, actualStatus);
            Assert.IsNotNull(result);
            Assert.AreEqual(user.UserName, result.UserName);
            Assert.AreEqual(user.Email, result.Email);
        }

        [TestMethod]
        public void CreateUser_PasswordIsInvalid_StatusAndUserMatchExpected()
        {
            // arrange
            const MembershipCreateStatus expectedStatus = MembershipCreateStatus.InvalidPassword;
            MembershipCreateStatus actualStatus;
            var user = CreateUser(expectedStatus);

            // act
            var result = _provider.CreateUser(user.UserName, user.Password, user.Email, "", "", true, null, out actualStatus);

            // assert
            Assert.AreEqual(expectedStatus, actualStatus);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateUser_EmailNotUnique_StatusAndUserMatchExpected()
        {
            // arrange
            const MembershipCreateStatus expectedStatus = MembershipCreateStatus.DuplicateEmail;
            MembershipCreateStatus actualStatus;
            var user = CreateUser(expectedStatus);
            _userRepo.Setup(x => x.GetUser(user.Email)).Returns(user);

            // act
            var result = _provider.CreateUser(user.UserName, user.Password, user.Email, "", "", true, null, out actualStatus);

            // assert
            Assert.AreEqual(expectedStatus, actualStatus);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateUser_UsernameNotUnique_StatusAndUserMatchExpected()
        {
            // arrange
            const MembershipCreateStatus expectedStatus = MembershipCreateStatus.DuplicateUserName;
            MembershipCreateStatus actualStatus;
            var user = CreateUser(expectedStatus);
            _userRepo.Setup(x => x.GetUser(user.UserName)).Returns(user);

            // act
            var result = _provider.CreateUser(user.UserName, user.Password, user.Email, "", "", true, null, out actualStatus);

            // assert
            Assert.AreEqual(expectedStatus, actualStatus);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateUser_RepoThrowsException_StatusAndUserMatchExpected()
        {
            // arrange
            const MembershipCreateStatus expectedStatus = MembershipCreateStatus.ProviderError;
            MembershipCreateStatus actualStatus;
            var user = CreateUser(expectedStatus);
            _userRepo.Setup(x => x.Save(It.IsAny<User>())).Throws(new InvalidOperationException());

            // act
            var result = _provider.CreateUser(user.UserName, user.Password, user.Email, "", "", true, null, out actualStatus);

            // assert
            Assert.AreEqual(expectedStatus, actualStatus);
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_ValidCall_ThrowsException()
        {
            // arrange

            // act
            _provider.DeleteUser("username", true);

            // assert
        }


        private User CreateUser(MembershipCreateStatus status)
        {
            var user = new User
            {
                Email = "test@createuser.com",
                UserName = "meepers57",
                Password = "V@l1dP@s5w0rd",
                Token = Guid.NewGuid().ToString(),
                UserRole = Role.Standard
            };

            if (status == MembershipCreateStatus.InvalidPassword)
            {
                user.Password = "a";
            }

            return user;
        }
    }
}
