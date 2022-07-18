using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Book
{
    public class IndexModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public IndexModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<BookLibrary.Models.BookIndex> Books { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchTitle { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchAuthor { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? PublishStartDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? PublishEndDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] SearchLocation { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] SearchSubcategory { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? SearchSeries { get; set; }
        [BindProperty(SupportsGet = true)]
        public Category? SearchCategory { get; set; }
        
        public async Task OnGetAsync()
        {
            if (_context.BookIndex != null)
            {
                IQueryable<BookIndex> BookSearch = _context.BookIndex;
                if (!String.IsNullOrEmpty(SearchTitle))
                {
                    BookSearch = BookSearch.Where(x => x.Title.Contains(SearchTitle));
                }
                if (!String.IsNullOrEmpty(SearchAuthor))
                {
                    BookSearch = BookSearch.Where(x => x.Authors.Contains(SearchAuthor) || x.Authors == null);
                }
                if (PublishStartDate != null)
                {
                    BookSearch = BookSearch.Where(x => x.Published >= PublishStartDate);
                }
                if (PublishEndDate != null)
                {
                    BookSearch = BookSearch.Where(x => x.Published <= PublishEndDate);
                }
                //if (!String.IsNullOrEmpty(SearchLocation))
                //{
                //    BookSearch = BookSearch.Where(x => x.Locations.Contains(SearchLocation) || x.Locations == null);
                //}
                //if (!String.IsNullOrEmpty(SearchSubcategory))
                //{
                //    BookSearch = BookSearch.Where(x => x.Subcategories.Contains(SearchSubcategory) || x.Subcategories == null);
                //}
                if (!String.IsNullOrEmpty(SearchSeries))
                {
                    BookSearch = BookSearch.Where(x => x.SeriesDisplay.Contains(SearchSeries) || x.SeriesDisplay == null);
                }
                if (SearchCategory != null)
                {
                    BookSearch = BookSearch.Where(x => x.Category == SearchCategory || x.Category == null);
                }
                Books = await BookSearch.ToListAsync();
            }
        }
    }
}
