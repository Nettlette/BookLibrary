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
    public class DeleteModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public DeleteModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookLibrary.Models.Reader Reader { get; set; } = default!;

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
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Readers == null)
            {
                return NotFound();
            }
            var reader = await _context.Readers.FindAsync(id);

            if (reader != null)
            {
                Reader = reader;
                _context.Readers.Remove(Reader);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
