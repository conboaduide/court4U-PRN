using BusinessLogic.Service;
using BusinessLogic.Service.Interface;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Migrations;
using DataAccess.Repository.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;

namespace Court4U.Pages
{
    public class BookCourtModel : PageModel
    {
        private readonly ISlotService _slotService;
        private readonly IClubService _clubService;
        private readonly IBillService _billService;
        private readonly IMomoService _momoService;

        public BookCourtModel(ISlotService slotService, IClubService clubService, IBillService billService, IMomoService momoService)
        {
            _slotService = slotService;
            _clubService = clubService;
            _billService = billService;
            _momoService = momoService;
        }

        public Club SelectedClub { get; set; }
        public List<Slot> Slots { get; set; }
        public string Message { get; set; }
        [BindProperty]
        public string SelectedSlotId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ClubId { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime SearchDate { get; set; } = DateTime.Now;
        public async Task<IActionResult> OnGetAsync()
        {
            if (ClubId.Equals("ClubId"))
            {
                ClubId = HttpContext.Session.GetString("ClubId");
            }
            else
            {
                HttpContext.Session.SetString("ClubId", ClubId);
            }
            if (string.IsNullOrEmpty(ClubId))
            {
                return RedirectToPage("/Clubs");
            }

            SelectedClub = await _clubService.GetClubByIdAsync(ClubId);
            if (SelectedClub == null)
            {
                return NotFound();
            }

            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(7);

            //Slots = await _slotService.GetAvailableSlotsAsync(ClubId, startDate, endDate);
            Slots = Slots.OrderBy(s => s.StartTime.Date).ThenBy(s => s.StartTime.TimeOfDay).ToList();

            return Page();
        }
        public async Task<IActionResult> OnPostSearchSlot(string ClubId, DateTime SearchDate)
        {
            if (ClubId.Equals("ClubId"))
            {
                ClubId = HttpContext.Session.GetString("ClubId");
            }
            else
            {
                HttpContext.Session.SetString("ClubId", ClubId);
            }
            if (string.IsNullOrEmpty(ClubId))
            {
                return RedirectToPage("/Clubs");
            }
            //Slots = await _slotService.SearchByDateAsync(SearchDate, ClubId);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string SelectedSlotId)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == null || userRole != "Member")
            {
                return RedirectToPage("/Login");
            }

            if (string.IsNullOrEmpty(SelectedSlotId))
            {
                Message = "Please select a slot.";
                return Page();
            }
            var slot = await _slotService.Get(SelectedSlotId);
            if (slot == null)
            {
                Message = "Slot is not available";
                return Page();
            }
            var ClubId = HttpContext.Session.GetString("ClubId");


            TempData["BookingMessage"] = Message;
            return Redirect($"/checkout?SelectedSlotId={SelectedSlotId}&ClubId={ClubId}&SearchDate={SearchDate}");
        }

        public async Task<IActionResult> OnGetSlot()
        {
            if (string.IsNullOrEmpty(ClubId))
            {
                return RedirectToPage("/Clubs");
            }

            SelectedClub = await _clubService.GetClubByIdAsync(ClubId);
            if (SelectedClub == null)
            {
                return NotFound();
            }

            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today.AddDays(7);

            //Slots = await _slotService.GetAvailableSlotsAsync(ClubId, startDate, endDate);
            Slots = Slots.OrderBy(s => s.StartTime.Date).ThenBy(s => s.StartTime.TimeOfDay).ToList();

            return Page();
        }
    }
}