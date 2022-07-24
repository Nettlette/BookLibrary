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
            Paging.CurrentPage = Paging.CurrentPage == 0 ? 1 : Paging.CurrentPage;
            CategoryList = new SelectList(PopulateDropdowns.GetCategories(), "Value", "Text", Category);
            StringBuilder param = new StringBuilder();
            if (_context.BooksRead != null)
            {
                IQueryable<BookLibrary.Models.BooksReadIndex> booksSearch = _context.BooksReadIndex;
                if (!String.IsNullOrEmpty(Title))
                {
                    booksSearch = booksSearch.Where(x => x.Title.Contains(Title));
                    param.Append($"&Title=").Append(Title);
                }
                if (!String.IsNullOrEmpty(Reader))
                {
                    booksSearch = booksSearch.Where(x => x.Reader.Contains(Reader));
                    param.Append($"&Reader=").Append(Reader);
                }
                if (!String.IsNullOrEmpty(Author))
                {
                    booksSearch = booksSearch.Where(x => x.Authors.Contains(Author));
                    param.Append($"&Author=").Append(Author);
                }
                if (StartDateFrom != null)
                {
                    booksSearch = booksSearch.Where(x => x.StartDate >= StartDateFrom);
                    param.Append($"&StartDateFrom=").Append(StartDateFrom);
                }
                if (StartDateTo != null)
                {
                    booksSearch = booksSearch.Where(x => x.StartDate <= StartDateTo);
                    param.Append($"&StartDateTo=").Append(StartDateTo);
                }
                if (EndDateFrom != null)
                {
                    booksSearch = booksSearch.Where(x => x.EndDate >= EndDateFrom);
                    param.Append($"&EndDateFrom=").Append(EndDateFrom);
                }
                if (EndDateTo != null)
                {
                    booksSearch = booksSearch.Where(x => x.EndDate <= EndDateTo);
                    param.Append($"&EndDateTo=").Append(EndDateTo);
                }
                if (NoStartDate)
                {
                    booksSearch = booksSearch.Where(x => x.StartDate == null);
                    param.Append($"&NoStartDate=").Append(NoStartDate);
                }
                if (NoEndDate)
                {
                    booksSearch = booksSearch.Where(x => x.EndDate == null);
                    param.Append($"&NoEndDate=").Append(NoEndDate);
                }
                if (Category != null)
                {
                    booksSearch = booksSearch.Where(x => x.Category == Category);
                    param.Append($"&Category=").Append(Category);
                }
                booksSearch = booksSearch.OrderBy(x => x.Title)
                                        .OrderBy(x => x.Reader)
                                        .OrderBy(x => x.EndDate);
                BooksRead = await booksSearch
                                        .Skip((Paging.CurrentPage - 1) * Paging.ItemsPerPage)
                                        .Take(Paging.ItemsPerPage)
                                        .ToListAsync();
                Paging.TotalItems = booksSearch.Count();
                Paging.CurrentPage = Math.Min(Paging.CurrentPage, Paging.TotalPages);
                Paging.Params = param.ToString();
            }
        }
    }
}
