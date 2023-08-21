using Microsoft.AspNetCore.Mvc;

namespace BookApplication.Controllers
{
    public class HelloWorldController : Controller
    {
        public string Index()
        {
            return "Default Hello World view";
        }

        // GET: /HelloWorld/Welcome/
        public string Welcome() {
            return "This is the Welcome method... ";
        } 
    }
}
