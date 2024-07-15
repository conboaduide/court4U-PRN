using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;

namespace Court4U.Pages.Owner.MemberSubscriptions
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.Entity.Court4UDbContext _context;

        public DeleteModel(DataAccess.Entity.Court4UDbContext context)
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

            var subscriptionoption = await _context.SubscriptionOptions.FirstOrDefaultAsync(m => m.Id == id);

            if (subscriptionoption == null)
            {
                return NotFound();
            }
            else
            {
                SubscriptionOption = subscriptionoption;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriptionoption = await _context.SubscriptionOptions.FindAsync(id);
            if (subscriptionoption != null)
            {
                SubscriptionOption = subscriptionoption;
                _context.SubscriptionOptions.Remove(SubscriptionOption);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
