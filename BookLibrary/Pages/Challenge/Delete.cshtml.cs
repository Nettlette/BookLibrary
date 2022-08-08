using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Challenge
{
    public class DeleteModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public DeleteModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public BookLibrary.Models.Challenge Challenge { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Challenge == null)
            {
                return NotFound();
            }

            var challenge = await _context.Challenge.FirstOrDefaultAsync(m => m.ChallengeId == id);

            if (challenge == null)
            {
                return NotFound();
            }
            else 
            {
                Challenge = challenge;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Challenge == null)
            {
                return NotFound();
            }
            var challenge = await _context.Challenge.FindAsync(id);

            if (challenge != null)
            {
                Challenge = challenge;
                _context.Challenge.Remove(Challenge);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
