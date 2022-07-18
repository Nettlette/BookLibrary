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

namespace BookLibrary.Pages.Author
{
    public class EditModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public EditModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookLibrary.Models.Author Author { get; set; } = default!;
        public SelectList Locations { get; set; }
        [BindProperty]
        public int[]? SelectedLocations { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Authors == null)
            {
                return NotFound();
            }

            var author =  await _context.Authors.FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            Author = author;
            SelectedLocations = _context.AuthorLocations.Where(x => x.AuthorId == id).Select(x => x.LocationId).ToArray();
            Locations = new SelectList(PopulateDropdowns.GetLocations(_context), "Value", "Text", SelectedLocations);
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

            _context.Attach(Author).State = EntityState.Modified;

            try
            {
                // Locations
                if (SelectedLocations != null)
                {
                    var oldItems = _context.AuthorLocations.Where(x => x.AuthorId == Author.AuthorId).ToList();
                    var newItems = SelectedLocations.Select(x => new AuthorLocation { AuthorId = Author.AuthorId, LocationId = x }).ToList();
                    var sameItems = oldItems.Where(x => SelectedLocations.Contains(x.LocationId)).ToList();
                    var removedItems = oldItems;
                    removedItems.RemoveAll(x => sameItems.Contains(x));
                    foreach (var a in sameItems)
                    {
                        newItems.Remove(newItems.First(x => x.LocationId == a.LocationId && x.AuthorId == a.AuthorId));
                    }
                    _context.AuthorLocations.AddRange(newItems);
                    _context.AuthorLocations.RemoveRange(removedItems);
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(Author.AuthorId))
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

        private bool AuthorExists(int id)
        {
          return (_context.Authors?.Any(e => e.AuthorId == id)).GetValueOrDefault();
        }
    }
}
