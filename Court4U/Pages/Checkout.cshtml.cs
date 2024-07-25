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

                var UserId = HttpContext.Session.GetString("UserId");
                var bill = new Bill
                {
                    Method = "Momo",
                    Price = slot.Price,
                    Type = "Booking",
                };
              
                var booking = new Booking
                {
                    BillId = bill.Id,
                    SlotId = slot.Id,
                    UserId = UserId,
                    Status = false,
                    Date = SearchDate,
                    Price = slot.Price,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                var result = await _slotService.Booking(booking, bill);

                if (result != null)
                {
                    var requestMomo = new RequestCreateOrderModel
                    {
                        Buy_date = DateTime.Now,
                        OrderId = result.Id,
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
                // Log or capture the inner exception details for troubleshooting
                var innerExceptionMessage = ex.InnerException?.Message ?? "No inner exception";
                throw new Exception($"An error occurred: {ex.Message}. Inner exception: {innerExceptionMessage}");
            }
        }
    }
}
