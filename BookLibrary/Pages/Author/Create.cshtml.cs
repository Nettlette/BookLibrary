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

namespace BookLibrary.Pages.Author
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
            Locations = new SelectList(PopulateDropdowns.GetLocations(_context), "Value", "Text");
            return Page();
        }

        [BindProperty]
        public BookLibrary.Models.Author Author { get; set; } = default!;
        public SelectList Locations { get; set; }
        [BindProperty]
        public int[]? SelectedLocations { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Authors == null || Author == null)
            {
                return Page();
            }

            _context.Authors.Add(Author);
            await _context.SaveChangesAsync();
            var newAuthor = _context.Authors.OrderBy(x => x.AuthorId).Last();
            foreach (var i in SelectedLocations)
            {
                _context.AuthorLocations.Add(new AuthorLocation { AuthorId = newAuthor.AuthorId, LocationId = i });
            }

            return RedirectToPage("./Index");
        }
    }
}
