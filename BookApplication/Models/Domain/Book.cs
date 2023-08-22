namespace BookApplication.Models.Domain
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; }
        public string Author { get; set; }
    }
}
