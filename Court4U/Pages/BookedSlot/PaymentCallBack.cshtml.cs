using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages.BookedSlot
{
    public class PaymentCallBackModel : PageModel
    {
        private readonly IBillService _billService;
        public PaymentCallBackModel (IBillService billService)
        {
            _billService = billService;
        }
        public async Task<IActionResult> OnGetAsync(string orderId)
        {
            Console.WriteLine(orderId);

            return Page();
        }
    }
}
