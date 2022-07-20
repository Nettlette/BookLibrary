using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Subcategory
{
    public class DetailsModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public DetailsModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public BookLibrary.Models.Subcategory Subcategory { get; set; } = default!; 
        public List<BookLibrary.Models.SubcategoryBookDetailView> Books { get; set; }
        public List<BookLibrary.Models.SubcategoryAuthorDetailView> Authors { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Subcategory == null)
            {
                return NotFound();
            }

            var subcategory = await _context.Subcategory.FirstOrDefaultAsync(m => m.SubcategoryId == id);
            if (subcategory == null)
            {
                return NotFound();
            }
            else 
            {
                Subcategory = subcategory;
                Books = await _context.SubcategoryBookDetailView.Where(x => x.SubcategoryId == id).OrderBy(x => x.Title).ToListAsync();
                Authors = await _context.SubcategoryAuthorDetailView.Where(x => x.SubcategoryId == id).OrderBy(x => x.AuthorName).ToListAsync();
            }
            return Page();
        }
    }
}
