using System.Web.Mvc;
using WheresMyMovies.Models;

namespace WheresMyMovies.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Standard")]
        public ActionResult Index()
        {
            return View();
        }

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