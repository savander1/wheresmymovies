using System;
using Moq;
using Xunit;
using wheresmymovies.Controllers;
using wheresmymovies.Data;

namespace test.UnitTests.Controllers
{
    public class AuthenticationControllerTests
    {
        private Mock<IAuthenticationService> _authService;
        private AuthenticationController _authController;

        public AuthenticationControllerTests()
        {
            _authService = new Mock<IAuthenticationService>();
            
            _authController = new AuthenticationController(_authService.Object);
        }
        
        [Fact]
        public void Ctr_AuthServiceNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => _authController = new AuthenticationController(null));
        }
    
    
    }
}
