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
    public class DetailsModel : PageModel
    {
        private readonly Court4UDbContext _context;

        public DetailsModel(Court4UDbContext context)
        {
            _context = context;
        }

        public Court Court { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Court = await _context.Courts
                .Include(c => c.Club)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Court == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
