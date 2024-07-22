using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace Court4U.Pages.Owner.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IMemberSubscriptionService _memberSubscriptionService;
        public IndexModel(IMemberSubscriptionService memberSubscriptionService)
        {
            _memberSubscriptionService = memberSubscriptionService;
        }
        public float TotalPrice { get; set; } = 0;
        public async Task<IActionResult> OnGetAsync()
        {
            var clubId = HttpContext.Session.Get("ClubId");
            var convertClubId = System.Text.Encoding.UTF8.GetString(clubId);
            var memberSubs = await _memberSubscriptionService.Get();
            var memberSubsList = memberSubs.Where(x => x.SubscriptionOption.ClubId == convertClubId).ToList();
            var memberSubListPrice = memberSubsList.Aggregate(0.0f, (acc, x) => acc + x.SubscriptionOption.price);
            TotalPrice += memberSubListPrice;
            return Page();
        }
    }
}
