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
using System.Text;

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
        public string Locations { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Subcategory { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Series { get; set; }
        [BindProperty(SupportsGet = true)]
        public Category? Category { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool IsBC { get; set; }
        public SelectList LocationsList { get; set; }
        public SelectList SubcategoriesList { get; set; }
        public SelectList CategoryList { get; set; }
        [BindProperty(SupportsGet = true)]
        public PagingInfo Paging { get; set; }

        public async Task OnGetAsync()
        {
            Paging.CurrentPage = Paging.CurrentPage == 0 ? 1 : Paging.CurrentPage;
            StringBuilder param = new StringBuilder();
            LocationsList = new SelectList(PopulateDropdowns.GetLocations(_context), "Text", "Text", Locations);
            SubcategoriesList = new SelectList(PopulateDropdowns.GetSubcategories(_context), "Text", "Text", Subcategory);
            CategoryList = new SelectList(PopulateDropdowns.GetCategories(), "Value", "Text", CategoryList);
            if (_context.BookIndex != null)
            {
                IQueryable<BookIndex> BookSearch = _context.BookIndex;
                if (!String.IsNullOrEmpty(Title))
                {
                    BookSearch = BookSearch.Where(x => x.Title.Contains(Title));
                    param.Append($"&Title=").Append(Title);
                }
                if (!String.IsNullOrEmpty(Author))
                {
                    BookSearch = BookSearch.Where(x => x.Authors.Contains(Author));
                    param.Append($"&Author=").Append(Author);
                }
                if (IsBC == true)
                {
                    BookSearch = BookSearch.Where(x => x.IsBC == true);
                    param.Append($"&IsBC=").Append(IsBC);
                    if (PublishStartDate != null)
                    {
                        BookSearch = BookSearch.Where(x => x.Published <= PublishStartDate);
                        param.Append($"&PublishStartDate=").Append(PublishStartDate);
                    }
                    if (PublishEndDate != null)
                    {
                        BookSearch = BookSearch.Where(x => x.Published >= PublishEndDate);
                        param.Append($"&PublishEndDate=").Append(PublishEndDate);
                    }
                }
                else
                {
                    if (PublishStartDate != null)
                    {
                        BookSearch = BookSearch.Where(x => x.Published >= PublishStartDate);
                        param.Append($"&PublishStartDate=").Append(PublishStartDate);
                    }
                    if (PublishEndDate != null)
                    {
                        BookSearch = BookSearch.Where(x => x.Published <= PublishEndDate);
                        param.Append($"&PublishEndDate=").Append(PublishEndDate);
                    }
                }
                if (!String.IsNullOrEmpty(Subcategory))
                {
                    BookSearch = BookSearch.Where(x => x.Subcategories.Contains(Subcategory));
                    param.Append($"&Subcategory=").Append(Subcategory);
                }
                if (!String.IsNullOrEmpty(Locations))
                {
                    BookSearch = BookSearch.Where(x => x.Locations.Contains(Locations));
                    param.Append($"&Locations=").Append(Locations);
                }
                if (!String.IsNullOrEmpty(Series))
                {
                    BookSearch = BookSearch.Where(x => x.SeriesName.Contains(Series));
                    param.Append($"&Series=").Append(Series);
                }
                if (Category != null)
                {
                    BookSearch = BookSearch.Where(x => x.Category == Category);
                    param.Append($"&Category=").Append(Category);
                }
                BookSearch = BookSearch.OrderBy(x => x.Title);
                Books = await BookSearch.Skip((Paging.CurrentPage - 1) * Paging.ItemsPerPage).Take(Paging.ItemsPerPage).ToListAsync();
                Paging.TotalItems = BookSearch.Count();
                Paging.CurrentPage = Math.Min(Paging.CurrentPage, Paging.TotalPages);
                Paging.Params = param.ToString();
            }
        }
    }
}
