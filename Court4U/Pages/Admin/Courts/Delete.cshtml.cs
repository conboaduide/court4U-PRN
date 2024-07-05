using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;

namespace Court4U.Pages.Admin.Courts
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.Entity.Court4UDbContext _context;

        public DeleteModel(DataAccess.Entity.Court4UDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Court Court { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Load Court including Club
            var court = await _context.Courts
                                    .Include(c => c.Club) // Ensure Club is included
                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (court == null)
            {
                return NotFound();
            }
            else
            {
                Court = court;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _context.Courts.FindAsync(id);
            if (court != null)
            {
                Court = court;
                _context.Courts.Remove(Court);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
