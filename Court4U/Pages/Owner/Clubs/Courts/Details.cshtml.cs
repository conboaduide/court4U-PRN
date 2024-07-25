using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;

namespace Court4U.Pages.Owner.Clubs.Courts
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Entity.Court4UDbContext _context;

        public DetailsModel(DataAccess.Entity.Court4UDbContext context)
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

            var court = await _context.Courts.FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
