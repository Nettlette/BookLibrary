using System.ComponentModel;

namespace BookLibrary.Models
{
    public class SeriesBookView
    {
        public int SeriesId { get;set; }
        public string Title { get;set; }
        public DateTime? Published { get; set; }
        public Category? Category { get; set; }
        [DisplayName("Order")]
        public decimal? SeriesOrder { get; set; }
        public string OrderDisplay { get { return String.Format("{0:#,0}", SeriesOrder); } }
    }
}
