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
    public class DeleteModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public DeleteModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookLibrary.Models.Series Series { get; set; } = default!;

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
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Series == null)
            {
                return NotFound();
            }
            var series = await _context.Series.FindAsync(id);

            if (series != null)
            {
                Series = series;
                _context.Series.Remove(Series);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
