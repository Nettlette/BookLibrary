namespace BookLibrary.Models
{
    public class LocationIndex
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public LocationType LocationType { get; set; }
        public int Books { get; set; }
        public int Authors { get; set; }
    }
}
