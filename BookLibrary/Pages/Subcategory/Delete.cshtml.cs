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
    public class DeleteModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public DeleteModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookLibrary.Models.Subcategory Subcategory { get; set; } = default!;

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
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Subcategory == null)
            {
                return NotFound();
            }
            var subcategory = await _context.Subcategory.FindAsync(id);

            if (subcategory != null)
            {
                Subcategory = subcategory;
                _context.Subcategory.Remove(Subcategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
