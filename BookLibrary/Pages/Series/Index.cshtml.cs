using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;
using BookLibrary.Models;

namespace BookLibrary.Pages.Series
{
    public class IndexModel : PageModel
    {
        private readonly BookLibrary.Data.ApplicationDbContext _context;

        public IndexModel(BookLibrary.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<BookLibrary.Models.Series> Series { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Series != null)
            {
                Series = await _context.Series.ToListAsync();
            }
        }
    }
}
