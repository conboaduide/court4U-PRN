using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Entity;
using DataAccess.Entity.Data;

namespace Court4U.Pages.Owner.Slots
{
    public class CreateModel : PageModel
    {
        private readonly DataAccess.Entity.Court4UDbContext _context;

        public CreateModel(DataAccess.Entity.Court4UDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClubId"] = new SelectList(_context.Clubs, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Slot Slot { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Slots.Add(Slot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
