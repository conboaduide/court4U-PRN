using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages.BookedSlot
{
    public class PaymentCallBackModel : PageModel
    {
        private readonly IBillService _billService;
        private readonly IBookingService _bookingService;
        private readonly IQRService _qrService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        public PaymentCallBackModel (IBillService billService, IBookingService bookingService, IQRService qrService, IEmailService emailService, IUserService userService)
        {
            _billService = billService;
            _bookingService = bookingService;
            _qrService = qrService;
            _emailService = emailService;
            _userService = userService;
        }
        public async Task<IActionResult> OnGetAsync(string orderId, string requestId)
        {
            var booking = await _bookingService.Get(requestId);
            if (booking == null)
            {
                throw new Exception("Not found booking");
            }
            var bill = await _billService.Get(booking.BillId);
            if (bill == null)
            {
                throw new Exception("Not found bill");
            }
            var user = await _userService.Get(booking.UserId);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            booking.Status = true;
            await _bookingService.Update(booking);
            var qrGen = _qrService.GenerateQRCode(booking.Id);
            var content = $"<h4>Date: </h4>{booking.Date}</br><h4>Price: </h4>{booking.Price}</br><img src=\"{qrGen}\"/>";
            await _emailService.SendQrCode(user.Email, content);
            return Page();
        }
    }
}
