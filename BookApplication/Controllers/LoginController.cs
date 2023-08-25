using Microsoft.AspNetCore.Mvc;

namespace BookApplication.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register() {
            return View();
        }
    }
}
