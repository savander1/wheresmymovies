using System;
using Microsoft.AspNet.Mvc;
using wheresmymovies.Data;

namespace wheresmymovies.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            if (authService == null) throw new ArgumentNullException(nameof(authService));
            _authService = authService;
        }

       
    }
}