using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;

namespace Court4U.Pages.Staff.CheckIn
{
    public class IndexModel : PageModel
    {
        IQRService _qrService;
        IBookingService _bookingService;

        public IndexModel(
            IQRService qrService,
            IBookingService bookingService)
        {
            _qrService = qrService;
            _bookingService = bookingService;
        }

        [BindProperty]
        public string QRCode { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public async Task<IActionResult> OnGet(string data)
        {
            //var qrCode = _qrService.GenerateQRCode("a168ac12-a2de-4c8e-98c5-620fc6b665b0");
            //QRCode = qrCode;

            var qrCodeContent = data;

            if (qrCodeContent != null)
            {
                var booking = await _bookingService.Get(data);

                if (booking.Date < DateTime.Now)
                {
                    // Ngay book da het han
                    Message = "Booking is expired";
                }
                else
                {
                    // Ngay book la hom nay hoac tuong lai
                    if (booking != null && booking.CheckIn == true)
                    {
                        Message = "Booking is already checked!";
                    }
                    else if (booking != null)
                    {
                        try
                        {
                            booking.CheckIn = true;
                            var result = await _bookingService.Update(booking);
                            Message = "Checked in successfully for booking " + data;
                        }
                        catch (Exception ex)
                        {
                            Message = "Error while checking in!";
                        }
                    }
                    else
                    {
                        Message = "Booking not found!";
                    }
                }
            }

            return Page();
        }
    }
}
