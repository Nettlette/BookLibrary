namespace BookLibrary.Models
{
    public class BookAuthor
    {
        public int BookAuthorID { get; set; }
        public Book Book { get; set; }
        public Book BookId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
    }
}
