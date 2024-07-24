using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using DataAccess.Repository.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
namespace Court4U.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ISlotService _slotService;
        private readonly IClubService _clubService;
        private readonly IBillService _billService;
        private readonly IMomoService _momoService;
        private readonly IBookingService _bookingService;

        public CheckoutModel(ISlotService slotService, IClubService clubService, IBillService billService, IMomoService momoService, IBookingService bookingService)
        {
            _slotService = slotService;
            _clubService = clubService;
            _billService = billService;
            _momoService = momoService;
            _bookingService = bookingService;
        }
        [BindProperty]
        public Slot slot { get; set; }
        [BindProperty]
        public string Price { get; set; }

        [BindProperty]
        public string SelectedSlotId { get; set; }

        [BindProperty]
        public string ClubId { get; set; }
        [BindProperty]
        public DateTime SearchDate { get; set; }
        public async Task<IActionResult> OnGetAsync(string selectedSlotId, string clubId, string searchDate)
        {
            slot = await _slotService.Get(selectedSlotId);
            Price = slot.Price.ToString("C0", new CultureInfo("vi-VN"));
            SearchDate = DateTime.Parse(searchDate);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var slot = await _slotService.Get(SelectedSlotId);
            try
            {

                var UserId = "";
                var BookingId = Guid.NewGuid().ToString();
                var bill = new Bill()
                {
                    Id = Guid.NewGuid().ToString(),
                    Method = "Momo",
                    Price = slot.Price,
                    Type = "Member",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };

                UserId = HttpContext.Session.GetString("UserId");


                var booking = new Booking()
                {
                    Id = BookingId,
                    Bill = bill,
                    BillId = bill.Id,
                    Status = false,
                    UserId = UserId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    Date = SearchDate
                };


                var result = await _bookingService.Create(booking);

                if (result != null)
                {
                    var requestMomo = new RequestCreateOrderModel
                    {
                        Buy_date = DateTime.Now,
                        OrderId = SelectedSlotId + DateTime.Now,
                        Price = slot.Price,
                        Type = "Momo",
                        UserId = UserId
                    };
                    var payment = await _momoService.CreateBookSlotPaymentAsync(requestMomo);
                    return Redirect(payment.PayUrl);
                }
                else
                {
                    return Page();
                }
            }
            catch (Exception ex)
            {
            }
            return Page();

        }
    }
}
