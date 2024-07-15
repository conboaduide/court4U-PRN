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

namespace Court4U.Pages.Owner.MemberSubscriptions
{
    public class EditModel : PageModel
    {
        private readonly DataAccess.Entity.Court4UDbContext _context;

        public EditModel(DataAccess.Entity.Court4UDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SubscriptionOption SubscriptionOption { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriptionoption =  await _context.SubscriptionOptions.FirstOrDefaultAsync(m => m.Id == id);
            if (subscriptionoption == null)
            {
                return NotFound();
            }
            SubscriptionOption = subscriptionoption;
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

            _context.Attach(SubscriptionOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriptionOptionExists(SubscriptionOption.Id))
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

        private bool SubscriptionOptionExists(string id)
        {
            return _context.SubscriptionOptions.Any(e => e.Id == id);
        }
    }
}
