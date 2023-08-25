using Microsoft.EntityFrameworkCore;
using BookApplication.Models.Domain;

namespace BookApplication.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {
        }

        // Add my properties
        public DbSet<Book> Books { get; set; }
		public DbSet<Users> Users { get; set; }
	}
}
