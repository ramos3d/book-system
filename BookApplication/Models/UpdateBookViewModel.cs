namespace BookApplication.Models
{
    public class UpdateBookViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; }
        public string Author { get; set; }
    }
}
