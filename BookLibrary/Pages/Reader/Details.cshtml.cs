using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Reader
{
    public class DetailsModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public DetailsModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public BookLibrary.Models.Reader Reader { get; set; } = default!;
        public List<BookLibrary.Models.ReaderLocationsView> Locations { get; set; }
        public List<BookLibrary.Models.ReaderAuthorsView> Authors { get; set; }
        public List<BookLibrary.Models.ReaderBooksView> Books { get; set; }
        public List<BookLibrary.Models.ReaderSubcategoryView> Subcategories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }

            var reader = await _context.Readers.FirstOrDefaultAsync(m => m.ReaderID == id);
            if (reader == null)
            {
                return NotFound();
            }
            else 
            {
                Reader = reader;
                Locations = await _context.ReaderLocationsView.Where(x => x.ReaderId == id).OrderBy(x => x.Name).ToListAsync();
                Authors = await _context.ReaderAuthorsView.Where(x => x.ReaderId == id).OrderBy(x => x.Name).ToListAsync();
                Books = await _context.ReaderBooksView.Where(x => x.ReaderId == id).OrderBy(x => x.Title).ToListAsync();
                Subcategories = await _context.ReaderSubcategoryView.Where(x => x.ReaderId == id).OrderBy(x => x.Name).ToListAsync();
            }
            return Page();
        }
    }
}
