using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder;

namespace Court4U.Pages.Staff.CheckIn
{
    public class IndexModel : PageModel
    {
        //IQRService _qrService;
        IBookingService _bookingService;

        public IndexModel(
            //IQRService qrService, 
            IBookingService bookingService)
        {
            //_qrService = qrService;
            _bookingService = bookingService;
        }

        [BindProperty]
        public string QRCode { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public async Task<IActionResult> OnGet(string data)
        {
            //var qrCode = _qrService.GenerateQRCode("2bfb7b23-acaf-4d8a-b9dc-ecb916ae9f9b");
            //QRCode = qrCode;

            var qrCodeContent = data;

            if (qrCodeContent != null)
            {
                var booking = await _bookingService.Get(data);

                if (booking != null)
                {
                    try
                    {
                        booking.Status = true;
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

            return Page();
        }
    }
}
