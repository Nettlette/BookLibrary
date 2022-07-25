using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BookLibrary.Data;
using BookLibrary.Models;
using BookLibrary.Extensions;

namespace BookLibrary.Pages.BooksRead
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
            Readers = new SelectList(PopulateDropdowns.GetReaders(_context), "Value", "Text");
            Books = new SelectList(PopulateDropdowns.GetBooks(_context), "Value", "Text");
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public BookLibrary.Models.BooksRead BooksRead { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int[] SelectedReaders { get; set; }
        [BindProperty(SupportsGet = true)]
        public int[] SelectedBooks { get; set; }
        public SelectList Readers { get; set; }
        public SelectList Books { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.BooksRead == null || BooksRead == null)
            {
                return Page();
            }
            List<BookLibrary.Models.BooksRead> br = new List<BookLibrary.Models.BooksRead>();
            foreach (var r in SelectedReaders)
            {
                foreach (var b in SelectedBooks)
                {
                    br.Add(new Models.BooksRead { BookId = b, ReaderId = r, StartDate = BooksRead.StartDate, EndDate = BooksRead.EndDate });
                }
            }

            _context.BooksRead.AddRange(br);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
