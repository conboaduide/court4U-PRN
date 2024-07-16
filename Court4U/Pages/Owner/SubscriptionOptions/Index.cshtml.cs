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
using BusinessLogic.Service;

namespace Court4U.Pages.Owner.SubscriptionOptions
{
    public class IndexModel : PageModel
    {
        private readonly ISubscriptionOptionService _subscriptionOptionService;

        public IndexModel(ISubscriptionOptionService _subscriptionOptionService)
        {
            this._subscriptionOptionService = _subscriptionOptionService;
        }

        public IList<SubscriptionOption> SubscriptionOption { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SubscriptionOption = await _subscriptionOptionService.Get();
            var subList = await _subscriptionOptionService.Get();
            var clubId = HttpContext.Session.Get("ClubId");
            var convertUserId = System.Text.Encoding.UTF8.GetString(clubId);
            SubscriptionOption = subList.Where(x => x.ClubId == convertUserId).ToList();
        }
    }
}
