using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;

namespace Court4U.Pages.Owner.Slots
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.Entity.Court4UDbContext _context;

        public DeleteModel(DataAccess.Entity.Court4UDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Slot Slot { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slot = await _context.Slots.FirstOrDefaultAsync(m => m.Id == id);

            if (slot == null)
            {
                return NotFound();
            }
            else
            {
                Slot = slot;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slot = await _context.Slots.FindAsync(id);
            if (slot != null)
            {
                Slot = slot;
                _context.Slots.Remove(Slot);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
