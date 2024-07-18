using BusinessLogic.Service;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Court4U.Pages
{
    public class CancelBookingModel : PageModel
    {
        private readonly IBookedSlotService _bookedSlotService;
        private readonly ISlotService _slotService;
        private readonly IClubService _clubService;
        public CancelBookingModel(IBookedSlotService bookedSlotService, ISlotService slotService, IClubService clubService)
        {
            _bookedSlotService = bookedSlotService;
            _slotService = slotService;
            _clubService = clubService;
        }

        [BindProperty]
        public string BookingId { get; set; }
        public string ClubName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Date { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            BookingId = id;
            var bookedSlot = await _bookedSlotService.Get(id);
            if (bookedSlot == null)
            {
                ModelState.AddModelError(string.Empty, "Booking not found.");
                return Page();
            }

            // Get the Slot details
            var slot = await _slotService.Get(bookedSlot.SlotId);
            if (slot == null)
            {
                ModelState.AddModelError(string.Empty, "Slot not found.");
                return Page();
            }

            // Get the Club details
            var club = await _clubService.GetClubByIdAsync(slot.ClubId);
            if (club == null)
            {
                ModelState.AddModelError(string.Empty, "Club not found.");
                return Page();
            }

            ClubName = club.Name;
            StartTime = slot.StartTime.ToString("HH:mm");
            EndTime = slot.EndTime.ToString("HH:mm");
            Date = slot.DateOfWeek.ToString("dd/MM/yyyy");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            var success = await _bookedSlotService.CancelBooking(BookingId, userId);
            if (success)
            {
                return RedirectToPage("/ViewBookedSlot");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to cancel booking.");
                return Page();
            }
        }
    }
}
