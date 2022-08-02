namespace BookLibrary.Models
{
    public class BooksPublished
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int Published { get; set; }
        public DateTime EndDate { get; set; }
        public int ReaderId { get; set; }
        public Category Category { get; set; }
    }
}
