namespace BookLibrary.Models
{
    public class ReaderBooksView
    {
        public int ReaderId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Title { get; set; }
        public DateTime? Published { get; set; }
        public Category? Category { get; set; }
        public decimal? SeriesOrder { get; set; }
        public string? SeriesName { get; set; }
        public string SeriesDisplay { get { return SeriesName != null ? SeriesName + " (#" + String.Format("{0:#,0}", SeriesOrder) + ")" : ""; } }

    }
}
