using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using wheresmymovies.Data;
using System;
using System.Linq;

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