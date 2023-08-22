using Microsoft.AspNetCore.Mvc;

namespace BookApplication.Controllers
{
    public class BooksController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
