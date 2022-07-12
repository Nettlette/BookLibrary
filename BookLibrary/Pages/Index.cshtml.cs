using BookLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        public int BooksWeek;
        public int BooksMonth;
        public int BooksYear;
        private DateTime StartOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        private DateTime StartOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        private DateTime StartOfYear = new DateTime(DateTime.Today.Year, 1, 1);

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            BooksWeek = _context.BooksRead.Count(x => x.EndDate >= StartOfWeek);
            BooksMonth = _context.BooksRead.Count(x => x.EndDate >= StartOfMonth);
            BooksYear = _context.BooksRead.Count(x => x.EndDate >= StartOfYear);
        }

        public void OnGet()
        {

        }
    }
}