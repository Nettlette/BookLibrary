namespace BookLibrary.Extensions
{
    public class PagingInfo
    {
        public PagingInfo(int itemsPerPage = 25)
        {
            itemsPerPage = itemsPerPage;
        }

        public int ItemsPerPage { get; set; }
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
