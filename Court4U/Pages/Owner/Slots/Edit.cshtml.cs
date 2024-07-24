using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;

namespace Court4U.Pages.Owner.Slots
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.Entity.Court4UDbContext _context;

        public EditModel(DataAccess.Entity.Court4UDbContext context)
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

            var slot =  await _context.Slots.FirstOrDefaultAsync(m => m.Id == id);
            if (slot == null)
            {
                return NotFound();
            }
            Slot = slot;
           ViewData["ClubId"] = new SelectList(_context.Clubs, "Id", "Id");
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

            _context.Attach(Slot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SlotExists(Slot.Id))
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

        private bool SlotExists(string id)
        {
            return _context.Slots.Any(e => e.Id == id);
        }
    }
}
