using BusinessLogic.Service.Interface;
using DataAccess.Repository.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace Court4U.Pages.Owner.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly IMemberSubscriptionService _memberSubscriptionService;
        private readonly IMomoService _momoService;
        public IndexModel(IMemberSubscriptionService memberSubscriptionService, IMomoService momoService)
        {
            _memberSubscriptionService = memberSubscriptionService;
            _momoService = momoService;

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

        public async Task<IActionResult> OnPostAsync()
        {
            var order = new RequestCreateOrderModel
            {
                Buy_date = DateTime.Now,
                OrderId = "abc11231op" + DateTime.Now.ToString(),
                Price = 120000,
                Type = "Member",
                UserId = "ed7bb1ea-5e99-44a0-9014-24c51c1a08ea"
            };
            var result = await _momoService.CreatePaymentAsync(order);
            return Redirect(result.PayUrl);
        }
    }
}
