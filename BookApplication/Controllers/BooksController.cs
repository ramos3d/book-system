using BookApplication.Data;
using BookApplication.Models;
using BookApplication.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BookApplication.Controllers
{
	public class BooksController : Controller
	{
		private readonly BookDbContext _bookDbContext;

		public BooksController(BookDbContext bookDbContext)
		{
			this._bookDbContext = bookDbContext;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var books_list = await _bookDbContext.Books.ToListAsync();
			return View(books_list);
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(AddBookViewModel addBookRequest)
		{
			try
			{
				var book = new Book()
				{
					Id = Guid.NewGuid(),
					Title = addBookRequest.Title,
					YearPublished = addBookRequest.YearPublished,
					Author = addBookRequest.Author
				};

				await _bookDbContext.AddAsync(book);
				await _bookDbContext.SaveChangesAsync();
				TempData["success"] = "Book added successfully!";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				TempData["error"] = "An error occurred while adding the book.";
				return RedirectToAction("Index");
			}
		}

		[HttpGet]
		public async Task<IActionResult> View(Guid id)
		{
			var book = await _bookDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

			if (book != null)
			{
				var viewModel = new UpdateBookViewModel()
				{
					Id = book.Id,
					Title = book.Title,
					YearPublished = book.YearPublished,
					Author = book.Author
				};
				return await Task.Run(() => View("View", viewModel));
			}
			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateBookViewModel model)
		{
			try
			{
				var book = await _bookDbContext.Books.FindAsync(model.Id);
				if (book != null)
				{
					book.Author = model.Author;
					book.Title = model.Title;
					book.YearPublished = model.YearPublished;
					await _bookDbContext.SaveChangesAsync();
				}
				TempData["success"] = "Book updated successfully!";
				return RedirectToAction("Index");
			}
			catch (Exception)
			{
				TempData["error"] = "An error occurred while updating the book.";
				return RedirectToAction("Index");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(UpdateBookViewModel model)
		{
			try
			{
				var book = await _bookDbContext.Books.FindAsync(model.Id);
				if (book != null)
				{
					_bookDbContext.Books.Remove(book);
					await _bookDbContext.SaveChangesAsync();
					TempData["success"] = "The book '" + model.Title + "' has been deleted sucessfully!";
				}
					return RedirectToAction("Index");
			}
			catch (Exception)
			{
				TempData["error"] = "Something went wrong!";
				return RedirectToAction("Index");
			}
		}
	}
}
