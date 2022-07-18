namespace BookLibrary.Models
{
    public class AuthorLocationsView
    {
        public int AuthorId { get; set; }
        public int LocationID { get; set; }
        public string Name { get; set; }
        public LocationType LocationType { get; set; }
    }
}
