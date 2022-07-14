using System.ComponentModel;

namespace BookLibrary.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        [DisplayName("Type")]
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
