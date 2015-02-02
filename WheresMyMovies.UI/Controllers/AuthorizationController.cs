using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WheresMyMovies.Controllers
{
    public class AuthorizationController : Controller
    {
        // GET: Authorization
        public ActionResult Login()
        {
            return View();
        }
    }
}