using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Subcategory
{
    public class CreateModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public CreateModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BookLibrary.Models.Subcategory Subcategory { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Subcategory == null || Subcategory == null)
            {
                return Page();
            }

            _context.Subcategory.Add(Subcategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
