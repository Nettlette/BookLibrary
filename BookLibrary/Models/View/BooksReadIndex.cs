using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class BooksReadIndex
    {
        public int BookId { get; set; }
        public int BooksReadId { get; set; }
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
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }
        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }
        public int ReaderId { get; set; }
        public string Reader { get; set; }
    }
}
