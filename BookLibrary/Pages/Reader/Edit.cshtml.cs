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

namespace BookLibrary.Pages.Reader
{
    public class EditModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public EditModel(BookLibrary.Data.ApplicationDbContext context)
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

            var reader =  await _context.Readers.FirstOrDefaultAsync(m => m.ReaderID == id);
            if (reader == null)
            {
                return NotFound();
            }
            Reader = reader;
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

            _context.Attach(Reader).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReaderExists(Reader.ReaderID))
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

        private bool ReaderExists(int id)
        {
          return (_context.Readers?.Any(e => e.ReaderID == id)).GetValueOrDefault();
        }
    }
}
