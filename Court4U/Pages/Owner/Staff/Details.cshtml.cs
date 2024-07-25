using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;

namespace Court4U.Pages.Owner.Staff
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccess.Entity.Court4UDbContext _context;

        public DetailsModel(DataAccess.Entity.Court4UDbContext context)
        {
            _context = context;
        }

        public StaffProfile StaffProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var staffprofile = await _context.StaffProfiles.Include(c=>c.User).Include(c=>c.Club).FirstOrDefaultAsync(m => m.Id == Id);
            if (staffprofile == null)
            {
                return NotFound();
            }
            else
            {
                StaffProfile = staffprofile;
            }
            return Page();
        }
    }
}
