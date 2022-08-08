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

namespace BookLibrary.Pages.Challenge
{
    public class EditModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public EditModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookLibrary.Models.Challenge Challenge { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public List<BookLibrary.Models.ChallengeElement> ChallengeElements { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Challenge == null)
            {
                return NotFound();
            }

            var challenge =  await _context.Challenge.FirstOrDefaultAsync(m => m.ChallengeId == id);
            if (challenge == null)
            {
                return NotFound();
            }
            Challenge = challenge;
            ChallengeElements = await _context.ChallengeElement.Where(x => x.ChallengeId == id).OrderBy(x => x.Order).ThenBy(x => x.Name).ToListAsync();
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

            _context.Attach(Challenge).State = EntityState.Modified;

            try
            {
                var oldElements = await _context.ChallengeElement.Where(x => x.ChallengeId == Challenge.ChallengeId).ToListAsync();
                var newItems = ChallengeElements.ToList();
                var newItemsIds = newItems.Select(x => x.ChallengeElementId).ToList();
                var sameItems = oldElements.Where(x => newItemsIds.Contains(x.ChallengeElementId)).ToList();
                var removedItems = oldElements;
                removedItems.RemoveAll(x => sameItems.Contains(x));
                foreach(var a in sameItems)
                {
                    newItems.Remove(newItems.First(x => x.ChallengeElementId == a.ChallengeElementId));
                    _context.Attach(a).State = EntityState.Modified;
                }
                foreach(var a in newItems)
                {
                    a.ChallengeId=Challenge.ChallengeId;
                }
                _context.ChallengeElement.AddRange(newItems);
                _context.ChallengeElement.RemoveRange(removedItems);
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeExists(Challenge.ChallengeId))
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

        private bool ChallengeExists(int id)
        {
          return (_context.Challenge?.Any(e => e.ChallengeId == id)).GetValueOrDefault();
        }
    }
}
