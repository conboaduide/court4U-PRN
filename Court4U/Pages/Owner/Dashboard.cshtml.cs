using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Repository.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Court4U.Pages.Owner
{
    public class DashboardModel : PageModel
    {
        private readonly IMemberSubscriptionService _memberSubscriptionService;
        private readonly IMomoService _momoService;
        private readonly IBillService _billService;
        private readonly IBookingService _bookingService;
        private readonly ICourtService _courtService;
        private readonly ISlotService _slotService;
       
        public DashboardModel(IMemberSubscriptionService memberSubscriptionService, IMomoService momoService, IBillService billService, IBookingService bookingService, ICourtService courtService, ISlotService slotService)
        {
            _memberSubscriptionService = memberSubscriptionService;
            _momoService = momoService;
            _billService = billService;
            _bookingService = bookingService;
            _courtService = courtService;
            _slotService = slotService;
        }
        [BindProperty]
        public string Price { get; set; }
        [BindProperty]
        public int MemberSubscriptionCount { get; set; }
        [BindProperty]
        public int BookingTodayCount { get; set; }
        [BindProperty]
        public int BookingInUse { get; set; }
        [BindProperty]
        public int CourtCount { get; set; }
        [BindProperty]
        public int SlotCount { get; set; }
        [BindProperty]
        public int[] CountBookingData {  get; set; }
        [BindProperty]
        public string[] CountBookingMonth { get; set; }
        [BindProperty]
        public int[] CountRevenueData { get; set; }
        [BindProperty]
        public string[] CountRevenueMonth { get; set; }

        public float TotalPrice { get; set; } = 0;

        public async Task<IActionResult> OnGetAsync()
        {
            var clubId = HttpContext.Session.Get("ClubId");
            var convertClubId = System.Text.Encoding.UTF8.GetString(clubId);

            // Calculate TotalPrice
            var memberSubs = await _memberSubscriptionService.Get();
            var memberSubsList = memberSubs.Where(x => x.SubscriptionOption.ClubId == convertClubId).ToList();
            var memberSubListPrice = memberSubsList.Aggregate(0.0f, (acc, x) => acc + x.SubscriptionOption.price);
            TotalPrice += memberSubListPrice;

            var booking = await _bookingService.Get();
            var billList = booking.Where(x => x.Slot.ClubId == convertClubId).ToList();
            var billListPrice = billList.Aggregate(0.0f, (acc, x) => acc + x.Price);
            TotalPrice += billListPrice;

            Price = TotalPrice.ToString("C0", new CultureInfo("vi-VN"));

            // Get counts
            var memberSub = await _memberSubscriptionService.GetByClubId(convertClubId);
            MemberSubscriptionCount = memberSub.Count;

            var bookingToday = await _bookingService.GetToDayBookingByClubId(convertClubId);
            BookingTodayCount = bookingToday.Count;

            var bookingInUseToday = await _bookingService.GetBookingInUseByClubId(convertClubId);
            BookingInUse = bookingInUseToday.Count;

            var court = await _courtService.GetCourtsByClubIdAsync(convertClubId);
            CourtCount = court.Count;

            var slot = await _slotService.Get();
            var list = slot.Where(x => x.ClubId == convertClubId).ToList();
            SlotCount = list.Count;

            CurrentYear[] bookingInCurrentYear = (CurrentYear[])await _bookingService.GetBookingInCurrentYear(convertClubId);
            CountBookingData  = bookingInCurrentYear.Select(x => x.Count).ToArray();
            CountBookingMonth = bookingInCurrentYear.Select(x => x.Month).ToArray();

            CurrentYear[] revenueInCurrentYear = (CurrentYear[])await _bookingService.GetBookingInCurrentYear(convertClubId);
            CountRevenueData = revenueInCurrentYear.Select(x => x.Count).ToArray();
            CountRevenueMonth = revenueInCurrentYear.Select(x => x.Month).ToArray();
            return Page();
        }

    }
}
