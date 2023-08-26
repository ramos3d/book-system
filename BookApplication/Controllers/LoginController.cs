using BookApplication.Data;
using BookApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace BookApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly BookDbContext _dbContext;
        private readonly Services.PasswordService _passwordService;

        public LoginController(BookDbContext dbContext, Services.PasswordService passwordService)
        {
            this._dbContext = dbContext;
            this._passwordService = passwordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (loginModel.email == null || loginModel.password == null)
            {
				TempData["error"] = "Email and password are required";
				return RedirectToAction("Index", "Login");
			}
            if (ModelState.IsValid)
            {
                // Checking user credentials
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginModel.email);

                if (user != null && _passwordService.VerifyPassword(loginModel.password, user.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Name),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["error"] = "Invalid email or password";
                    return RedirectToAction("Index", "Login");
                }
            }

            return View(loginModel); 
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}
