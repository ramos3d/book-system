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
		private readonly BookDbContext bookDbContext;

		public BooksController(BookDbContext bookDbContext) 
        {
			this.bookDbContext = bookDbContext;
		}
        [HttpGet]
        public async Task<IActionResult> Index() 
        { 
            var books_list = await bookDbContext.Books.ToListAsync();
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
            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = addBookRequest.Title,
                YearPublished = addBookRequest.YearPublished,
                Author = addBookRequest.Author
            };

			await bookDbContext.AddAsync(book);
            await bookDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
		}

        [HttpGet] 
        public async Task<IActionResult> View(Guid id) 
        {
            var book =await bookDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

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
            var book = await bookDbContext.Books.FindAsync(model.Id);
            if (book != null)
            {
                book.Author = model.Author;
                book.Title = model.Title;   
                book.YearPublished = model.YearPublished;
                await bookDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateBookViewModel model)
        { 
            var book = await bookDbContext.Books.FindAsync(model.Id);
            if (book != null)
            {
                bookDbContext.Books.Remove(book);
                await bookDbContext.SaveChangesAsync();
                TempData["success"] = "Book "+ model.Title +" has been deleted sucessfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Something went wrong!";
            return RedirectToAction("Index");

        }
    }
}
