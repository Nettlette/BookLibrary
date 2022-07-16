using BookLibrary.Data;
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
        public string? Author { get; set; }
        public DateTime Published { get; set; }
        [NotMapped] 
        public string? Locations { get; set; }
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
        public string? Subcategories { get; set; }
        public Series? Series { get; set; }
        public int? SeriesId { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}")]
        public decimal? SeriesOrder { get; set; }
        [NotMapped]
        public string SeriesDisplay { get { return Series != null ? Series.Name + " (#" + String.Format("{0:#,0}", SeriesOrder) + ")" : ""; } }
    }

    public enum Category
    {
        Fiction,
        Nonfiction
    }
}
