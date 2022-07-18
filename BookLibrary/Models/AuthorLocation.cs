namespace BookLibrary.Models
{
    public class AuthorLocation
    {
        public int AuthorLocationId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Location Location { get; set; }
        public int LocationId { get; set; }
    }
}
