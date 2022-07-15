using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrary.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        [NotMapped]
        public List<Author>? Author { get; set; }
        public DateTime Published { get; set; }
        [NotMapped] 
        public List<Location>? Locations { get; set; }
        public int? Pages { get; set; }
        public decimal? Hours { get; set; }
        public string? ISBN { get; set; }
        public string? ASIN { get; set; }
        [NotMapped]
        public int PublishedYear { get { return Published.Year; } }
        [NotMapped]
        public int PublishedDecade => Convert.ToInt32(Math.Floor((double)Published.Year / 10.0) * 10);
        public Category? Category { get; set; }
        [NotMapped] 
        public List<Subcategory>? Subcategories { get; set; }
        public Series? Series { get; set; }
        public int? SeriesId { get; set; }
        public decimal? SeriesOrder { get; set; }
    }

    public enum Category
    {
        Fiction,
        Nonfiction
    }
}
