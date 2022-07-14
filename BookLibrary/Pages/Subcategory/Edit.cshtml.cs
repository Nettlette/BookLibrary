using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Subcategory
{
    public class EditModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public EditModel(BookLibrary.Data.ApplicationDbContext context)
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

            var subcategory =  await _context.Subcategory.FirstOrDefaultAsync(m => m.SubcategoryId == id);
            if (subcategory == null)
            {
                return NotFound();
            }
            Subcategory = subcategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Subcategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubcategoryExists(Subcategory.SubcategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SubcategoryExists(int id)
        {
          return (_context.Subcategory?.Any(e => e.SubcategoryId == id)).GetValueOrDefault();
        }
    }
}
