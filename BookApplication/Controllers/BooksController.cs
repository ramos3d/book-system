using BookApplication.Data;
using BookApplication.Models;
using BookApplication.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BookApplication.Controllers
{
    public class BooksController : Controller
    {
		private readonly BookDbContext bookDbContext;

		public BooksController(BookDbContext bookDbContext) 
        {
			this.bookDbContext = bookDbContext;
		}

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel addBookRequest) 
        {
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = addBookRequest.Title,
                YearPublished = addBookRequest.YearPublished,
                Author = addBookRequest.Author
            };

			await bookDbContext.AddAsync(book);
            await bookDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
		}
    }
}
