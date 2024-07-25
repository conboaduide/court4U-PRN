using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Court4U.Pages.Owner.Clubs.Slots
{
    public class IndexModel : PageModel
    {
        private readonly ISlotService _slotService;

        public IndexModel(ISlotService slotService)
        {
            _slotService = slotService;
        }

        public List<TimeSlot> TimeSlots { get; set; }

        public List<Slot> Slots { get; set; }

        [BindProperty]
        public string ClubId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var clubId = HttpContext.Session.GetString("ClubId");
            if (clubId.IsNullOrEmpty())
            {
                return RedirectToPage("/Owner/Clubs/Index");
            }

            var slots = await _slotService.GetSlotsByClubId(clubId);
            TimeSlots = GenerateTimeSlots(slots);
            Slots = slots;
            ClubId = clubId;
            return Page();
        }

        private List<TimeSlot> GenerateTimeSlots(List<Slot> slots)
        {
            var timeSlotMap = new Dictionary<string, Dictionary<int, SlotInfo>>();
            var uniqueTimes = new HashSet<string>();

            foreach (var slot in slots)
            {
                var startTime = slot.StartTime.ToString("HH:mm");
                var endTime = slot.EndTime.ToString("HH:mm");
                var dayOfWeek = (int)slot.DateOfWeek;

                uniqueTimes.Add(startTime);

                if (!timeSlotMap.ContainsKey(startTime))
                {
                    timeSlotMap[startTime] = new Dictionary<int, SlotInfo>();
                }

                timeSlotMap[startTime][dayOfWeek] = new SlotInfo
                {
                    SlotId = slot.Id.ToString(),
                    Display = $"{startTime} - {endTime}",
                    Start = startTime,
                    End = endTime,
                    Price = slot.Price.ToString()
                };
            }

            var timeSlotArray = uniqueTimes
                .Select(time => new TimeSlot
                {
                    Time = time,
                    Slots = timeSlotMap[time]
                })
                .OrderBy(ts => ts.Time)
                .ToList();

            return timeSlotArray;
        }

        public class TimeSlot
        {
            public string Time { get; set; }
            public Dictionary<int, SlotInfo> Slots { get; set; }
        }

        public class SlotInfo
        {
            public string SlotId { get; set; }
            public string Display { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public string Price { get; set; }
        }

        public async Task<IActionResult> OnPostAddSlot(DateTime StartTime, DateTime EndTime, float Price, int DayOfWeek, string ClubId)
        {
            var slot = new Slot
            {
                Id = Guid.NewGuid().ToString(),
                DateOfWeek = (DataAccess.Entity.Enums.DateOfWeek)DayOfWeek,
                ClubId = ClubId,
                Price = Price,
                StartTime = StartTime,
                EndTime = EndTime,
            };
            await _slotService.Create(slot);
            return Redirect("/Owner/Clubs/Slots?clubId=" + ClubId);
        }

        public async Task<IActionResult> OnPostUpdateSlot(DateTime StartTime, DateTime EndTime, float Price, int DayOfWeek, string SlotId)
        {
            var slot = await _slotService.Get(SlotId);

            if (slot == null)
            {
                TempData["Message"] = "Slot not exist";
            }
            else
            {
                slot.DateOfWeek = (DataAccess.Entity.Enums.DateOfWeek)DayOfWeek;
                slot.Price = Price;
                slot.StartTime = StartTime;
                slot.EndTime = EndTime;
                await _slotService.Update(slot);
            }

            return Redirect("/Owner/Clubs/Slots?clubId=" + ClubId);
        }

        public async Task<IActionResult> OnPostDeleteSlot(string slotId, string ClubId)
        {
            await _slotService.Delete(slotId);
            return Redirect("/Owner/Clubs/Slots?clubId=" + ClubId);
        }
    }
}
