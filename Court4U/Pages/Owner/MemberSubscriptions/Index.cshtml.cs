using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages.Owner.MemberSubscriptions
{
    public class IndexModel : PageModel
    {
        private readonly IMemberSubscriptionService _memberSubscriptionService;
        public IndexModel(IMemberSubscriptionService memberSubscriptionService)
        {
            this._memberSubscriptionService = memberSubscriptionService;
        }
        public IList<MemberSubscription> MemberSubscriptions { get; set; }
        public async Task OnGetAsync()
        {
            MemberSubscriptions = await _memberSubscriptionService.Get();
        }
    }
}
