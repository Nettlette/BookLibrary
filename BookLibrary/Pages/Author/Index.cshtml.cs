using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;
using BookLibrary.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.ComponentModel;

namespace BookLibrary.Pages.Author
{
    public class IndexModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public IndexModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<BookLibrary.Models.AuthorIndex> Author { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("Start")]
        public DateTime? DateBornStart { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("End")]
        public DateTime? DateBornEnd { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("No Date Born")]
        public bool NoDateBorn { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("Start")]
        public DateTime? DateDiedStart { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("End")]
        public DateTime? DateDiedEnd { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("No Date Died")]
        public bool NoDateDied { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Locations { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("Book Count")]
        public int? BookCount { get; set; }
        [BindProperty(SupportsGet = true)]
        public PagingInfo Paging { get; set; }
        public SelectList LocationsList { get; set; }

        public async Task OnGetAsync()
        {
            LocationsList = new SelectList(PopulateDropdowns.GetLocations(_context), "Text", "Text", Locations);
            Paging.CurrentPage = Paging.CurrentPage == 0 ? 1 : Paging.CurrentPage;
            StringBuilder param = new StringBuilder();
            if (_context.Authors != null)
            {
                IQueryable<BookLibrary.Models.AuthorIndex> authorSearch = _context.AuthorIndex;
                if (!String.IsNullOrEmpty(Name))
                {
                    authorSearch = authorSearch.Where(x => x.Name.Contains(Name));
                    param.Append($"&Name=").Append(Name);
                }
                if (DateBornStart != null)
                {
                    authorSearch = authorSearch.Where(x => x.DateBorn >= DateBornStart);
                    param.Append($"&DateBornStart=").Append(DateBornStart);
                }
                if (DateBornEnd != null)
                {
                    authorSearch = authorSearch.Where(x => x.DateBorn <= DateBornEnd);
                    param.Append($"&DateBornEnd=").Append(DateBornEnd);
                }
                if (DateDiedStart != null)
                {
                    authorSearch = authorSearch.Where(x => x.DateDied >= DateDiedStart);
                    param.Append($"&DateDiedStart=").Append(DateDiedStart);
                }
                if (DateDiedEnd != null)
                {
                    authorSearch = authorSearch.Where(x => x.DateDied <= DateDiedEnd);
                    param.Append($"&DateDiedEnd=").Append(DateDiedEnd);
                }
                if (NoDateBorn)
                {
                    authorSearch = authorSearch.Where(x => x.DateBorn == null);
                    param.Append($"&NoDateBorn=").Append(NoDateBorn);
                }
                if (NoDateDied)
                {
                    authorSearch = authorSearch.Where(x => x.DateDied == null);
                    param.Append($"&NoDateDied=").Append(NoDateDied);
                }
                if (!String.IsNullOrEmpty(Locations))
                {
                    authorSearch = authorSearch.Where(x => x.Locations.Contains(Locations));
                    param.Append($"&Locations=").Append(Locations);
                }
                if (BookCount != null)
                {
                    authorSearch = authorSearch.Where(x => x.Count == BookCount);
                    param.Append($"&BookCount=").Append(BookCount);
                }
                authorSearch = authorSearch.OrderBy(x => x.Name);
                Author = await authorSearch
                                .Skip((Paging.CurrentPage - 1) * Paging.ItemsPerPage)
                                .Take(Paging.ItemsPerPage)
                                .ToListAsync();
                Paging.TotalItems = authorSearch.Count();
                Paging.CurrentPage = Math.Min(Paging.CurrentPage, Paging.TotalPages);
                Paging.Params = param.ToString();
            }
        }
    }
}
