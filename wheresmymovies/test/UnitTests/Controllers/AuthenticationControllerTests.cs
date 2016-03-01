using System.Collections.Generic;
using System.Linq;
using Moq;
using wheresmymovies.Data;
using wheresmymovies.Controllers;
using Xunit;
using wheresmymovies.Models;
using wheresmymovies.Entities;
using System;
using System.Net;

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
