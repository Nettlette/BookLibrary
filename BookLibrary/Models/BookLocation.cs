namespace BookLibrary.Models
{
    public class BookLocation
    {
        public int BookLocationID { get; set; }
        public Book Book { get; set; }
        public Book BookId { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
    }
}
