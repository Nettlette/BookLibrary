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
        public string? Title { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Author { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("Start Date")]
        public DateTime? PublishStartDate { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("End Date")]
        public DateTime? PublishEndDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] Locations { get; set; }
        [BindProperty(SupportsGet = true)]
        public string[] Subcategories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Series { get; set; }
        [BindProperty(SupportsGet = true)]
        public Category? Category { get; set; }
        public SelectList LocationsList { get; set; }
        public SelectList SubcategoriesList { get; set; }
        public SelectList CategoryList { get; set; }
        
        public async Task OnGetAsync()
        {
            LocationsList = new SelectList(PopulateDropdowns.GetLocations(_context), "Text", "Text", Locations);
            SubcategoriesList = new SelectList(PopulateDropdowns.GetSubcategories(_context), "Text", "Text", Subcategories);
            CategoryList = new SelectList(PopulateDropdowns.GetCategories(), "Value", "Text", CategoryList);
            if (_context.BookIndex != null)
            {
                IQueryable<BookIndex> BookSearch = _context.BookIndex;
                if (!String.IsNullOrEmpty(Title))
                {
                    BookSearch = BookSearch.Where(x => x.Title.Contains(Title));
                }
                if (!String.IsNullOrEmpty(Author))
                {
                    BookSearch = BookSearch.Where(x => x.Authors.Contains(Author) || x.Authors == null);
                }
                if (PublishStartDate != null)
                {
                    BookSearch = BookSearch.Where(x => x.Published >= PublishStartDate);
                }
                if (PublishEndDate != null)
                {
                    BookSearch = BookSearch.Where(x => x.Published <= PublishEndDate);
                }
                foreach (var l in Locations)
                {
                    BookSearch = BookSearch.Where(x => x.Locations.Contains(l) || x.Locations == null);
                }
                //if (!String.IsNullOrEmpty(SearchSubcategory))
                //{
                //    BookSearch = BookSearch.Where(x => x.Subcategories.Contains(SearchSubcategory) || x.Subcategories == null);
                //}
                if (!String.IsNullOrEmpty(Series))
                {
                    BookSearch = BookSearch.Where(x => x.SeriesDisplay.Contains(Series) || x.SeriesDisplay == null);
                }
                if (Category != null)
                {
                    BookSearch = BookSearch.Where(x => x.Category == Category || x.Category == null);
                }
                Books = await BookSearch.ToListAsync();
            }
        }
    }
}
