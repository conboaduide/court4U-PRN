using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages
{
    public class ViewBookedSlotModel : PageModel
    {
        private readonly IBookedSlotService _bookedSlotService;
        public ViewBookedSlotModel(IBookedSlotService bookedSlotService)
        {
            _bookedSlotService = bookedSlotService;
        }
        public IList<DataAccess.Entity.Data.BookedSlot> BookedSlotList { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            BookedSlotList = await _bookedSlotService.GetByUserId(userId);
            return Page();
        }
    }
}