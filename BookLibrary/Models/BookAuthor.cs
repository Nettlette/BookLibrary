namespace BookLibrary.Models
{
    public class BookAuthor
    {
        public int BookAuthorID { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }

        public bool Compare (BookAuthor ba)
        {
            return ba.AuthorId == this.AuthorId && ba.BookId == this.BookId;
        }
    }
}
