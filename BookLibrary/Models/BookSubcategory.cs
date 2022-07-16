namespace BookLibrary.Models
{
    public class BookSubcategory
    {
        public int BookSubcategoryId { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Subcategory Subcategory { get; set; }
        public int SubcategoryId { get; set; }
    }
}
