namespace BookLibrary.Models
{
    public class BookDetailsByAuthorView
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public DateTime? Published { get; set; }
        public int? Pages { get; set; }
        public decimal? Hours { get; set; }
        public string? ISBN { get; set; }
        public string? ASIN { get; set; }
        public Category? Category { get; set; }
        public int? SeriesId { get; set; }
        public decimal? SeriesOrder { get; set; }
        public bool IsBC { get; set; }
        public string? Name { get; set; }
        public string? Locations { get; set; }
        public string? Subcategories { get; set; }
        public string? SeriesDisplay { get { return Name != null ? Name + " (#" + String.Format("{0:#,0}", SeriesOrder) + ")" : ""; } }
    }
}
