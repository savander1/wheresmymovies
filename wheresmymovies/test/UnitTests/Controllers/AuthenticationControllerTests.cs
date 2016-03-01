using System.Collections.Generic;
using System.Linq;
using Moq;
using wheresmymovies.Data;
using wheresmymovies.Controllers;
using Xunit;
using wheresmymovies.Models;
using wheresmymovies.Entities;
using System.Net;

namespace test.UnitTests.Controllers
{
    public class AuthenticationControllerTests
    {
        private AutenticationController _authController;

        protected void TestInitialize()
        {
            _authController = new AutenticationController(null);
        }
    }
}
