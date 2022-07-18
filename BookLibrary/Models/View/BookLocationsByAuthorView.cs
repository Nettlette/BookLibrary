namespace BookLibrary.Models
{
    public class BookLocationsByAuthorView
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public LocationType LocationType { get; set; }
    }
}
