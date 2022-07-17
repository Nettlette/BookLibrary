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
using BookLibrary.Extensions;

namespace BookLibrary.Pages.Book
{
    public class EditModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public EditModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book =  await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            Category = new SelectList(PopulateDropdowns.GetCategories(), "Value", "Text");
            Authors = new SelectList(PopulateDropdowns.GetAuthors(_context), "Value", "Text");
            Locations = new SelectList(PopulateDropdowns.GetLocations(_context), "Value", "Text");
            Subcategories = new SelectList(PopulateDropdowns.GetSubcategories(_context), "Value", "Text");
            Series = new SelectList(PopulateDropdowns.GetSeries(_context), "Value", "Text");
            Book = book;
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

            _context.Attach(Book).State = EntityState.Modified;

            try
            {
                // Authors update
                if (SelectedAuthors != null)
                {
                    var oldItems = _context.BookAuthors.Where(x => x.BookId == Book.BookId).ToList();
                    var newItems = SelectedAuthors.Select(x => new BookAuthor { BookId = Book.BookId, AuthorId = x }).ToList();
                    var sameItems = oldItems.Where(x => SelectedAuthors.Contains(x.AuthorId)).ToList();
                    var removedItems = oldItems;
                    removedItems.RemoveAll(x => sameItems.Contains(x));
                    foreach(var a in sameItems)
                    {
                        newItems.Remove(newItems.First(x => x.AuthorId == a.AuthorId && x.BookId == a.BookId));
                    }
                    _context.BookAuthors.AddRange(newItems);
                    _context.BookAuthors.RemoveRange(removedItems);
                }
                // Locations
                if (SelectedLocations != null)
                {
                    var oldItems = _context.BookLocations.Where(x => x.BookId == Book.BookId).ToList();
                    var newItems = SelectedLocations.Select(x => new BookLocation { BookId = Book.BookId, LocationId = x }).ToList();
                    var sameItems = oldItems.Where(x => SelectedLocations.Contains(x.LocationId)).ToList();
                    var removedItems = oldItems;
                    removedItems.RemoveAll(x => sameItems.Contains(x));
                    foreach (var a in sameItems)
                    {
                        newItems.Remove(newItems.First(x => x.LocationId == a.LocationId && x.BookId == a.BookId));
                    }
                    _context.BookLocations.AddRange(newItems);
                    _context.BookLocations.RemoveRange(removedItems);
                }
                // Subcategories
                if (SelectedSubcategories != null)
                {
                    var oldItems = _context.BookSubcategories.Where(x => x.BookId == Book.BookId).ToList();
                    var newItems = SelectedSubcategories.Select(x => new BookSubcategory { BookId = Book.BookId, SubcategoryId = x }).ToList();
                    var sameItems = oldItems.Where(x => SelectedSubcategories.Contains(x.SubcategoryId)).ToList();
                    var removedItems = oldItems;
                    removedItems.RemoveAll(x => sameItems.Contains(x));
                    foreach (var a in sameItems)
                    {
                        newItems.Remove(newItems.First(x => x.SubcategoryId == a.SubcategoryId && x.BookId == a.BookId));
                    }
                    _context.BookSubcategories.AddRange(newItems);
                    _context.BookSubcategories.RemoveRange(removedItems);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(Book.BookId))
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

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.BookId == id)).GetValueOrDefault();
        }
    }
}
