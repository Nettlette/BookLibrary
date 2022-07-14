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

namespace BookLibrary.Pages.BooksRead
{
    public class EditModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public EditModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookLibrary.Models.BooksRead BooksRead { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BooksRead == null)
            {
                return NotFound();
            }

            var booksread =  await _context.BooksRead.FirstOrDefaultAsync(m => m.BooksReadId == id);
            if (booksread == null)
            {
                return NotFound();
            }
            BooksRead = booksread;
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

            _context.Attach(BooksRead).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BooksReadExists(BooksRead.BooksReadId))
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

        private bool BooksReadExists(int id)
        {
          return (_context.BooksRead?.Any(e => e.BooksReadId == id)).GetValueOrDefault();
        }
    }
}
