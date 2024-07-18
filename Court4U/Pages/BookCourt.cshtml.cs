using BusinessLogic.Service;
using BusinessLogic.Service.Interface;
using DataAccess.Entity;
using DataAccess.Entity.Data;
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

        public BookCourtModel(ISlotService slotService, IClubService clubService, IBillService billService)
        {
            _slotService = slotService;
            _clubService = clubService;
            _billService = billService;
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

            Slots = await _slotService.GetAvailableSlotsAsync(ClubId, startDate, endDate);
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
            Slots = await _slotService.SearchByDateAsync(SearchDate, ClubId);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string ClubId, string SelectedSlotId)
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
            try
            {
                var UserId = "";
                var BookingId = Guid.NewGuid().ToString();
                var bill = new Bill()
                {
                    Id = Guid.NewGuid().ToString(),
                    Method = "",
                    Price = 0,
                    Type = "",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };

                UserId = HttpContext.Session.GetString("UserId");

                var booking = new Booking()
                {
                    Id = BookingId,
                    Bill = bill,
                    Status = true,
                    UserId = UserId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };
                var bookedSlot = new BookedSlot()
                {
                    Id = Guid.NewGuid().ToString(),
                    CheckedIn = false,
                    SlotId = SelectedSlotId,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    BookingId = BookingId,
                };

                var result = await _slotService.BookSlotAsync(ClubId, SelectedSlotId, booking, bookedSlot);

                if (result)
                {
                    Message = "Booking successful!";
                }
                else
                {
                    Message = "Booking failed. Please try again.";
                }
            }
            catch (Exception ex)
            {
                Message = "Booking failed due to an error. Please try again later.";
            }

            TempData["BookingMessage"] = Message;

            return Page();
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

            Slots = await _slotService.GetAvailableSlotsAsync(ClubId, startDate, endDate);
            Slots = Slots.OrderBy(s => s.StartTime.Date).ThenBy(s => s.StartTime.TimeOfDay).ToList();

            return Page();
        }
    }
}