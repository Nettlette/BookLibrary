namespace BookLibrary.Models
{
    public class LocationAuthorDetailView
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public LocationType LocationType { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
