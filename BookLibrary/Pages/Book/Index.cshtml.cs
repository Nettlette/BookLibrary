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

        public List<BookLibrary.Models.Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Books != null)
            {
                Book = await _context.Books.Include(x => x.Series).ToListAsync();
                foreach(var b in Book)
                {
                    var a = _context.BookAuthors.Where(x => x.BookId == b.BookId).Select(x => x.AuthorId);
                    var authors = _context.Authors.Where(x => a.Contains(x.AuthorId)).Select(x => x.Name).ToList();
                    b.Author = String.Join(", ", authors);
                    var l = _context.BookLocations.Where(x => x.BookId == b.BookId).Select(x => x.LocationId);
                    var locations = _context.Locations.Where(x => l.Contains(x.LocationID)).Select(x => x.Name).ToList();
                    b.Locations = String.Join(", ", locations);
                    var s = _context.BookSubcategories.Where(x => x.BookId == b.BookId).Select(x => x.SubcategoryId);
                    var subcategories = _context.Subcategory.Where(x => s.Contains(x.SubcategoryId)).Select(x => x.Name).ToList();
                    b.Subcategories = String.Join(", ", subcategories);
                }
            }
        }
    }
}
