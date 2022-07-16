using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Author
{
    public class IndexModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public IndexModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<BookLibrary.Models.Author> Author { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Authors != null)
            {
                Author = await _context.Authors.OrderBy(x => x.Name).ToListAsync();
            }
        }
    }
}
