using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Author
{
    public class DetailsModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public DetailsModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public BookLibrary.Models.Author Author { get; set; } = default!;
        public List<BookLibrary.Models.AuthorLocationsView> AuthorLocations { get; set; }
        public List<BookLibrary.Models.BookLocationsByAuthorView> BookLocations { get; set; }
        public List<BookLibrary.Models.BookDetailsByAuthorView> Books { get; set; }
        public List<BookLibrary.Models.BookSubcategoriesByAuthorView> BookSubcategories { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            else 
            {
                Author = author;
                AuthorLocations = await _context.AuthorLocationsView.Where(x => x.AuthorId == id).ToListAsync();
                BookLocations = await _context.BookLocationsByAuthorView.Where(x => x.AuthorId == id).ToListAsync();
                Books = await _context.BookDetailsByAuthorView.Where(x => x.AuthorId == id).ToListAsync();
                BookSubcategories = await _context.BookSubcategoriesByAuthorView.Where(x => x.AuthorId == id).ToListAsync();
            }
            return Page();
        }
    }
}
