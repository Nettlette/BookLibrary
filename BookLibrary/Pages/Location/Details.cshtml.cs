using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Location
{
    public class DetailsModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public DetailsModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public BookLibrary.Models.Location Location { get; set; } = default!;
        public List<BookLibrary.Models.LocationBookDetailView> Books { get; set; }
        public List<BookLibrary.Models.LocationAuthorDetailView> Authors { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FirstOrDefaultAsync(m => m.LocationID == id);
            if (location == null)
            {
                return NotFound();
            }
            else 
            {
                Location = location;
                Books = await _context.LocationBookDetailView.Where(x => x.LocationId == id).OrderBy(x => x.Title).ToListAsync();
                Authors = await _context.LocationAuthorDetailView.Where(x => x.LocationId == id).OrderBy(x => x.AuthorName).ToListAsync();
            }
            return Page();
        }
    }
}
