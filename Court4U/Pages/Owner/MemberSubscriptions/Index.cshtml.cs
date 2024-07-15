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

namespace Court4U.Pages.Owner.MemberSubscriptions
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
        }
    }
}
