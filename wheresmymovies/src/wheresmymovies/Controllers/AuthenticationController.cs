using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using wheresmymovies.Entities;
using wheresmymovies.Models;
using wheresmymovies.Data;
using System.Linq;

namespace wheresmymovies.Controllers
{
    [Route("api/[controller]")]
    public class AutenticationController : Controller
    {
        private readonly IAuthenticationService _authService;

        public AutenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

       
    }
}