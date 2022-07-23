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
using Microsoft.AspNetCore.Mvc.Rendering;
using BookLibrary.Extensions;

namespace BookLibrary.Pages.BooksRead
{
    public class IndexModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public IndexModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BookLibrary.Models.BooksReadIndex> BooksRead { get;set; } = default!;
        public SelectList CategoryList { get; set; }
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
        [BindProperty(SupportsGet = true)]
        public Category? Category { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Author { get; set; }
        [BindProperty(SupportsGet = true)]
        public PagingInfo Paging { get; set; }

        public async Task OnGetAsync()
        {
            CategoryList = new SelectList(PopulateDropdowns.GetCategories(), "Value", "Text", Category);
            if (_context.BooksRead != null)
            {
                IQueryable<BookLibrary.Models.BooksReadIndex> booksSearch = _context.BooksReadIndex;
                if (!String.IsNullOrEmpty(Title))
                {
                    booksSearch = booksSearch.Where(x => x.Title.Contains(Title));
                }
                if (!String.IsNullOrEmpty(Reader))
                {
                    booksSearch = booksSearch.Where(x => x.Reader.Contains(Reader));
                }
                if (!String.IsNullOrEmpty(Author))
                {
                    booksSearch = booksSearch.Where(x => x.Authors.Contains(Author));
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
                if (Category != null)
                {
                    booksSearch = booksSearch.Where(x => x.Category == Category);
                }
                BooksRead = await booksSearch.OrderBy(x => x.Title).OrderBy(x => x.Reader).OrderBy(x => x.EndDate).ToListAsync();
                Paging.TotalItems = BooksRead.Count;
            }
        }
    }
}
