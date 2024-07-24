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

        public async Task<IActionResult> OnGetAsync(string? clubId)
        {
            if (clubId.IsNullOrEmpty())
            {
                return RedirectToPage("/Owner/Clubs/Index");
            }

            var slots = await _slotService.GetSlotsByClubId(clubId);
            TimeSlots = GenerateTimeSlots(slots);
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
                    Display = $"{startTime} - {endTime}"
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
        }
    }
}
