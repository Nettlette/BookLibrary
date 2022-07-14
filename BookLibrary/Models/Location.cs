namespace BookLibrary.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public LocationType LocationType { get; set; }
    }

    public enum LocationType
    {
        State,
        Country,
        Continent,
        Other
    }
}
