namespace BookLibrary.Models
{
    public class PagesHoursByAuthorView
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
        public decimal Hours { get; set; }
    }
}
