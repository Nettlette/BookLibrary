using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;
using System.ComponentModel;

namespace BookLibrary.Pages.BooksRead
{
    public class IndexModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public IndexModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BookLibrary.Models.BooksRead> BooksRead { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? Title { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Reader { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("Start Date From")]
        public DateTime? StartDateFrom { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("Start Date To")]
        public DateTime? StartDateTo { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("End Date From")]
        public DateTime? EndDateFrom { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("End Date To")]
        public DateTime? EndDateTo { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("No Start Date")]
        public bool NoStartDate { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("No End Date")]
        public bool NoEndDate { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.BooksRead != null)
            {
                IQueryable<BookLibrary.Models.BooksRead> booksSearch = _context.BooksRead.Include(x => x.Book).Include(x => x.Reader);
                if (!String.IsNullOrEmpty(Title))
                {
                    booksSearch = booksSearch.Where(x => x.Book.Title.Contains(Title));
                }
                if (!String.IsNullOrEmpty(Reader))
                {
                    booksSearch = booksSearch.Where(x => x.Reader.Name.Contains(Reader));
                }
                if (StartDateFrom != null)
                {
                    booksSearch = booksSearch.Where(x => x.StartDate >= StartDateFrom);
                }
                if (StartDateTo != null)
                {
                    booksSearch = booksSearch.Where(x => x.StartDate <= StartDateTo);
                }
                if (EndDateFrom != null)
                {
                    booksSearch = booksSearch.Where(x => x.EndDate >= EndDateFrom);
                }
                if (EndDateTo != null)
                {
                    booksSearch = booksSearch.Where(x => x.EndDate <= EndDateTo);
                }
                if (NoStartDate)
                {
                    booksSearch = booksSearch.Where(x => x.StartDate == null);
                }
                if (NoEndDate)
                {
                    booksSearch = booksSearch.Where(x => x.EndDate == null);
                }
                BooksRead = await booksSearch.OrderBy(x => x.Book.Title).OrderBy(x => x.Reader).OrderBy(x => x.EndDate).ToListAsync();
            }
        }
    }
}
