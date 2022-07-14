using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.BooksRead
{
    public class DetailsModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public DetailsModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public BookLibrary.Models.BooksRead BooksRead { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.BooksRead == null)
            {
                return NotFound();
            }

            var booksread = await _context.BooksRead.FirstOrDefaultAsync(m => m.BooksReadId == id);
            if (booksread == null)
            {
                return NotFound();
            }
            else 
            {
                BooksRead = booksread;
            }
            return Page();
        }
    }
}
