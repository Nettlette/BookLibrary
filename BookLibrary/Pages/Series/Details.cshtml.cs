using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Series
{
    public class DetailsModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public DetailsModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public BookLibrary.Models.Series Series { get; set; } = default!; 
        public List<BookLibrary.Models.SeriesAuthorView> Authors { get; set; }
        public List<BookLibrary.Models.SeriesBookView> Books { get; set; }
        public List<BookLibrary.Models.SeriesSubcategoryView> Subcategories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Series == null)
            {
                return NotFound();
            }

            var series = await _context.Series.FirstOrDefaultAsync(m => m.SeriesId == id);
            if (series == null)
            {
                return NotFound();
            }
            else 
            {
                Series = series;
                Books = await _context.SeriesBookView.Where(x => x.SeriesId == id).OrderBy(x => x.SeriesOrder).ToListAsync();
                Authors = await _context.SeriesAuthorView.Where(x => x.SeriesId == id).OrderBy(x => x.Name).ToListAsync();
                Subcategories = await _context.SeriesSubcategoryView.Where(x => x.SeriesId == id).OrderBy(x => x.Name).ToListAsync();
            }
            return Page();
        }
    }
}
