using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using BookApplication.Models;
using BookApplication.Models.Domain;
using BookApplication.Data;

namespace BookApplication.Controllers
{
	public class UserHistoryController : Controller
	{
		private readonly BookDbContext _bookDbContext;

		public UserHistoryController(BookDbContext bookDbContext)
		{
			this._bookDbContext = bookDbContext;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost, Authorize]
		public async Task<IActionResult> AddFavoriteBook(Guid bookId)
		{
			var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Authenticated user
			var userHistory = await _bookDbContext.User_History.FirstOrDefaultAsync(uh => uh.Users_Id == userId);

			if (userHistory == null)
			{
				userHistory = new UserHistory
				{
					Id = Guid.NewGuid(),
					Users_Id = userId,
					To_Buy = JsonConvert.SerializeObject(new List<Guid>()), // initialize an empty Json
					Owned_books = JsonConvert.SerializeObject(new List<Guid>())
				};
				_bookDbContext.User_History.Add(userHistory);
			}

			// Now add the ID into the toBuyList
			var toBuyList = JsonConvert.DeserializeObject<List<Guid>>(userHistory.To_Buy);
			if (!toBuyList.Contains(bookId))
			{
				toBuyList.Add(bookId);
				userHistory.To_Buy = JsonConvert.SerializeObject(toBuyList);
			}
			await _bookDbContext.SaveChangesAsync();
			return RedirectToAction("Index", "Books");
		}

		[HttpPost, Authorize]
		public async Task<IActionResult> AddOwnedBook(Guid bookId)
		{
			try
			{
				var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
				var userHistory = await _bookDbContext.User_History.FirstOrDefaultAsync(uh => uh.Users_Id == userId);

				if (userHistory == null)
				{
					userHistory = new UserHistory
					{
						Id = Guid.NewGuid(),
						Users_Id = userId,
						To_Buy = JsonConvert.SerializeObject(new List<Guid>()),
						Owned_books = JsonConvert.SerializeObject(new List<Guid>())
					};
					_bookDbContext.User_History.Add(userHistory);
				}

				var ownedBooksList = JsonConvert.DeserializeObject<List<Guid>>(userHistory.Owned_books);
				if (!ownedBooksList.Contains(bookId))
				{
					ownedBooksList.Add(bookId);
					userHistory.Owned_books = JsonConvert.SerializeObject(ownedBooksList);
				}

				await _bookDbContext.SaveChangesAsync();
				return RedirectToAction("Index", "Books");
			}
			catch (Exception ex)
			{
				return View("Error", (ex));
			}
		}

		[Authorize, HttpGet]
		public async Task<IActionResult> ToBuyBooks()
		{
			try
			{
				var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
				var userHistory = await _bookDbContext.User_History.FirstOrDefaultAsync(uh => uh.Users_Id == userId);

				if (userHistory == null)
				{
					return View(new List<Book>());
				}

				var toBuyBooksIds = JsonConvert.DeserializeObject<List<string>>(userHistory.To_Buy);
				var toBuyBooks = await _bookDbContext.Books
					.Where(b => toBuyBooksIds.Contains(b.Id.ToString()))
					.ToListAsync();
				return View("ToBuyBooks", toBuyBooks);
			}
			catch (Exception ex)
			{
				return View("Error", ex);
			}
		}

		[Authorize, HttpGet]
		public async Task<IActionResult> OwnedBooks()
		{
			try
			{
				var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
				var userHistory = await _bookDbContext.User_History.FirstOrDefaultAsync(uh => uh.Users_Id == userId);

				if (userHistory == null)
				{
					return View(new List<Book>());
				}
				var ownedBooksIds = JsonConvert.DeserializeObject<List<string>>(userHistory.Owned_books);
				var ownedBooks = await _bookDbContext.Books
					.Where(b => ownedBooksIds.Contains(b.Id.ToString()))
					.ToListAsync();
				return View("OwnedBooks", ownedBooks);
			}
			catch (Exception ex)
			{
				return View("Error", ex);
			}
		}
	}
}
