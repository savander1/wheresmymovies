using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WheresMyMovies.Data.Repository;
using WheresMyMovies.Entities;

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
    }
}
