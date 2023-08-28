using BookApplication.Models.Domain;

namespace BookApplication.Models
{
    public class UserHistoryViewModel
    {
        public Guid Users_Id { get; set; }
        public string To_Buy { get; set; }
        public List<Book> Owned_Books { get; set; }
    }
}
