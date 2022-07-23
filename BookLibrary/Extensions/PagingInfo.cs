namespace BookLibrary.Extensions
{
    public class PagingInfo
    {
        public PagingInfo() { }

        public int ItemsPerPage { get; set; } = 25;
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
            }
        }
    }
}
