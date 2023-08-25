using BookApplication.Data;
using BookApplication.Models;
using BookApplication.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BookApplication.Controllers
{
    public class RegisterController : Controller
	{
		private readonly BookDbContext _dbContext;
		private readonly Services.PasswordService _passwordService;
		public RegisterController(BookDbContext dbContext, Services.PasswordService passwordService)
		{
			this._dbContext = dbContext;
			this._passwordService = passwordService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddRegister(AddUserViewModel addUser) {
			try
			{
				var user = new Users()
				{
					Id = Guid.NewGuid(),
					Name = addUser.name,
					Email = addUser.email,
					Password = this._passwordService.HashPassword(addUser.password)
				};

				await _dbContext.AddAsync(user);
				await _dbContext.SaveChangesAsync();

				TempData["success"] = "User registered successfully";
				return RedirectToAction("Index", "Login");
			}
			catch (Exception ex)
			{
				TempData["error"] = "An error occurred while registering the user." + ex;
				return RedirectToAction("Register", "Login");
			}
		}
	}
}
