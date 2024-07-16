using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using BusinessLogic.Service.Interface;

namespace Court4U.Pages.Owner.SubscriptionOptions
{
    public class ActiveModel : PageModel
    {
        private readonly ISubscriptionOptionService subscriptionOptionService;

        public ActiveModel(ISubscriptionOptionService isubscriptionOptionService)
        {
            this.subscriptionOptionService = isubscriptionOptionService;
        }

        [BindProperty]
        public SubscriptionOption SubscriptionOption { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscriptionoption = await subscriptionOptionService.Get(id);

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

            var subscriptionoption = await subscriptionOptionService.Get(id);
            if (subscriptionoption != null)
            {
                SubscriptionOption = subscriptionoption;
                SubscriptionOption.Status = Enums.SubscriptionOptionStatus.Active;
                await subscriptionOptionService.Update(SubscriptionOption);
            }

            return RedirectToPage("./Index");
        }
    }
}
