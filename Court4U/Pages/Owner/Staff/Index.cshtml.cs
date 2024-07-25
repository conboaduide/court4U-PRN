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
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Entity.Court4UDbContext _context;

        public IndexModel(DataAccess.Entity.Court4UDbContext context)
        {
            _context = context;
        }

        public IList<StaffProfile> StaffProfile { get;set; } = default!;

        public async Task OnGetAsync()
        {
            StaffProfile = await _context.StaffProfiles
                .Include(s => s.Club)
                .Include(s => s.User).ToListAsync();
        }
    }
}
