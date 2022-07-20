namespace BookLibrary.Models
{
    public class SubcategoryBookDetailView
    {
        public int SubcategoryId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string? SeriesName { get; set; }
        public decimal? SeriesOrder { get; set; }
        public string Authors { get; set; }
        public DateTime Published { get; set; }
        public Category? Category { get; set; }
        public string SeriesDisplay { get { return SeriesName != null ? SeriesName + " (#" + String.Format("{0:#,0}", SeriesOrder) + ")" : ""; } }
    }
}
