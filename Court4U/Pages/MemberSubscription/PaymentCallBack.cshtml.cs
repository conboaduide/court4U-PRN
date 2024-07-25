using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages.MemberSubscription
{
    public class PaymentCallBackModel : PageModel
    {
        private readonly IMemberSubscriptionService _memberSubscription;
        private readonly ISubscriptionOptionService _subscriptionOptionService;
        public PaymentCallBackModel(IMemberSubscriptionService memberSubscriptionServicem, ISubscriptionOptionService subscriptionOptionService)
        {
            _memberSubscription = memberSubscriptionServicem;
            _subscriptionOptionService = subscriptionOptionService;
        }

        public async Task<IActionResult> OnGetAsync(string orderId, string requestId, string extraData)
        {
            string[] extraDataArray = extraData.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var subscription = await _subscriptionOptionService.Get(extraDataArray[0]);
            if (subscription == null)
            {
                throw new Exception("Subscription not found");
            }
            var currentTime = DateTime.Now;
            var memberSubs = new DataAccess.Entity.Data.MemberSubscription()
            {
                BillId = extraDataArray[1],
                StartDate = currentTime,
                EndDate = currentTime.AddDays(subscription.TotalDate),
                MemberId = requestId,
                SubscriptionOptionId = subscription.Id,
                Price = subscription.price,
            };

            var result = await _memberSubscription.Create(memberSubs);
            return Page();
        }
    }
}
