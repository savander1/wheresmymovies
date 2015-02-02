using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheresMyMovies.Models;

namespace WheresMyMovies.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel loginModel)
        {
            
            return View();
        }
    }
}