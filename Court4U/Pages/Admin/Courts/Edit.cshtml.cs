using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Entity.Data;
using BusinessLogic.Service.Interface;
using DataAccess.Repository.Request;
using System.Threading.Tasks;
using static DataAccess.Entity.Enums;

namespace Court4U.Pages.Admin.Courts
{
    public class EditModel : PageModel
    {
        private readonly ICourtService _courtService;
        private readonly IClubService _clubService;

        public EditModel(ICourtService courtService, IClubService clubService)
        {
            _courtService = courtService;
            _clubService = clubService;
        }

        [BindProperty]
        public CourtRequest CourtRequest { get; set; }

        public SelectList ClubOptions { get; set; }
        public SelectList StatusOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var court = await _courtService.Get(id);
            if (court == null)
            {
                return NotFound();
            }

            CourtRequest = new CourtRequest
            {
                Num = court.Num,
                Status = court.Status,
                ClubId = court.ClubId
            };

            StatusOptions = new SelectList(Enum.GetValues(typeof(CourtStatus)).Cast<CourtStatus>());
            var clubs = await _clubService.GetAllClubsAsync();
            ClubOptions = new SelectList(clubs, nameof(Club.Id), nameof(Club.Name));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                StatusOptions = new SelectList(Enum.GetValues(typeof(CourtStatus)).Cast<CourtStatus>());
                var clubs = await _clubService.GetAllClubsAsync();
                ClubOptions = new SelectList(clubs, nameof(Club.Id), nameof(Club.Name));
                return Page();
            }

            var courtToUpdate = await _courtService.Get(id);
            if (courtToUpdate == null)
            {
                return NotFound();
            }

            // Check if CourtRequest.Num is provided and different from current Num
            if (CourtRequest.Num != courtToUpdate.Num)
            {
                var allCourts = await _courtService.Get();
                if (allCourts != null && allCourts.Any(c => c.Num == CourtRequest.Num && c.ClubId == CourtRequest.ClubId))
                {
                    ModelState.AddModelError("CourtRequest.Num", "Court number already exists in this club.");
                    StatusOptions = new SelectList(Enum.GetValues(typeof(CourtStatus)).Cast<CourtStatus>());
                    var clubs = await _clubService.GetAllClubsAsync();
                    ClubOptions = new SelectList(clubs, nameof(Club.Id), nameof(Club.Name));
                    return Page();
                }
            }

            // Update court fields
            courtToUpdate.Num = CourtRequest.Num;
            courtToUpdate.Status = CourtRequest.Status;
            courtToUpdate.ClubId = CourtRequest.ClubId;

            await _courtService.Update(courtToUpdate);

            return RedirectToPage("./Index");
        }

    }
}
