using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace BookApplication.Controllers
{
    public class AddController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
