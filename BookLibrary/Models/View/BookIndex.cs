using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class BookIndex
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        [DisplayFormat(DataFormatString = "{0:M/d/yyyy}")]
        public DateTime? Published { get; set; }
        public int? Pages { get; set; }
        public decimal? Hours { get; set; }
        public Category? Category { get; set; }
        public string? SeriesName { get; set; }
        public decimal? SeriesOrder { get; set; }
        public string? Authors { get; set; }
        public string? Subcategories { get; set; }
        public string? Locations { get; set; }
        public string SeriesDisplay { get { return SeriesName != null ? SeriesName + " (#" + String.Format("{0:#,0}", SeriesOrder) + ")" : ""; } }
    }
}
