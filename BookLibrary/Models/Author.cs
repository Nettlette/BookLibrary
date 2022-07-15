using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrary.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        [DisplayName("Date Born")]
        public DateTime? DateBorn { get; set; }
        [DisplayName("Date Died")]
        public DateTime? DateDied { get; set; }
        [NotMapped]
        public List<Location> Locations { get; set; }
    }
}
