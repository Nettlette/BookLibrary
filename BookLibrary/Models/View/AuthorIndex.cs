namespace BookLibrary.Models
{
    public class AuthorIndex
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public DateTime? DateBorn { get; set; }
        public DateTime? DateDied { get; set; }
        public int? Age { get; set; }
        public string Locations { get; set; }
        public int Count { get; set; }
    }
}
