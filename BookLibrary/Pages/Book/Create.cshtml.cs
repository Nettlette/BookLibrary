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

namespace BookLibrary.Pages.Book
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
            Category = new SelectList(PopulateDropdowns.GetCategories(), "Value", "Text");
            Authors = new SelectList(PopulateDropdowns.GetAuthors(_context), "Value", "Text");
            Locations = new SelectList(PopulateDropdowns.GetLocations(_context), "Value", "Text");
            Subcategories = new SelectList(PopulateDropdowns.GetSubcategories(_context), "Value", "Text");
            Series = new SelectList(PopulateDropdowns.GetSeries(_context), "Value", "Text");
            return Page();
        }

        [BindProperty]
        public BookLibrary.Models.Book Book { get; set; } = default!;
        public SelectList Category { get; set; }
        public SelectList Authors { get; set; }
        public SelectList Locations { get; set; }
        public SelectList Subcategories { get; set; }
        public SelectList Series { get; set; }
        [BindProperty]
        public int[]? SelectedAuthors { get; set; }
        [BindProperty] 
        public int[]? SelectedLocations { get; set; }
        [BindProperty]
        public int[]? SelectedSubcategories { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Books == null || Book == null)
            {
                return Page();
            }

            _context.Books.Add(Book);
            await _context.SaveChangesAsync();
            var newBook = _context.Books.OrderBy(x => x.BookId).Last();
            foreach(var i in SelectedAuthors)
            {
                _context.BookAuthors.Add(new BookAuthor { AuthorId = i, BookId = newBook.BookId });
            }
            foreach(var i in SelectedLocations)
            {
                _context.BookLocations.Add(new BookLocation { BookId = newBook.BookId, LocationId = i });
            }
            foreach(var i in SelectedSubcategories)
            {
                _context.BookSubcategories.Add(new BookSubcategory { BookId = newBook.BookId, SubcategoryId = i });
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
