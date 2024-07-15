using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using BusinessLogic.Service.Interface;
using BusinessLogic.Service;
using System.Collections;

namespace Court4U.Pages.Owner.MemberSubscriptions
{
    public class CreateModel : PageModel
    {
        private readonly ISubscriptionOptionService _subscriptionOptionService;
        private readonly IClubService _clubService;

        public CreateModel(ISubscriptionOptionService iSubscriptionOptionService, IClubService iclubService)
        {
            _subscriptionOptionService = iSubscriptionOptionService;
            _clubService = iclubService;
        }


        [BindProperty]
        public SubscriptionOption SubscriptionOption { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var userId = HttpContext.Session.Get("UserId");
                var convertUserId = System.Text.Encoding.UTF8.GetString(userId);
                var club = await _clubService.GetClubByUserIdAsync(convertUserId);
                SubscriptionOption.ClubId = club?.Id;
                SubscriptionOption.Status = Enums.SubscriptionOptionStatus.Active;
                var result = await _subscriptionOptionService.Create(SubscriptionOption);
                if (result != null)
                {
                    return RedirectToPage("./Index");
                }
                ModelState.AddModelError(string.Empty, "Invalid Username or Password.");
                return Page();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
