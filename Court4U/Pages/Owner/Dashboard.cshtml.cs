using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Repository.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace Court4U.Pages.Owner
{
    public class DashboardModel : PageModel
    {
        private readonly IMemberSubscriptionService _memberSubscriptionService;
        private readonly IMomoService _momoService;
        private readonly IBillService _billService;
        private readonly IBookingService _bookingService;
        public DashboardModel(IMemberSubscriptionService memberSubscriptionService, IMomoService momoService, IBillService billService, IBookingService bookingService)
        {
            _memberSubscriptionService = memberSubscriptionService;
            _momoService = momoService;
            _billService = billService;
            _bookingService = bookingService;
        }
        [BindProperty]
        public string Price { get; set; }
        public float TotalPrice { get; set; } = 0;
        public async Task<IActionResult> OnGetAsync()
        {
            var clubId = HttpContext.Session.Get("ClubId");
            var convertClubId = System.Text.Encoding.UTF8.GetString(clubId);
            var memberSubs = await _memberSubscriptionService.Get();
            var memberSubsList = memberSubs.Where(x => x.SubscriptionOption.ClubId == convertClubId).ToList();
            var memberSubListPrice = memberSubsList.Aggregate(0.0f, (acc, x) => acc + x.SubscriptionOption.price);
            TotalPrice += memberSubListPrice;
            var booking = await _bookingService.Get();
            var billList = booking.Where(x => x.Slot.ClubId == convertClubId).ToList();
            var billListPrice = billList.Aggregate(0.0f, (acc, x) => acc + x.Price);
            TotalPrice += billListPrice;
            Price = TotalPrice.ToString("C0", new CultureInfo("vi-VN"));
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
            var result = await _momoService.CreateBookSlotPaymentAsync(order);
            return Redirect(result.PayUrl);
        }
    }
}
