using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Service.Interface;
using DataAccess.Repository.Request;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Entity.Data;
using Microsoft.AspNetCore.Mvc;
using static DataAccess.Entity.Enums;

namespace Court4U.Pages.Admin.Courts
{
    public class CreateModel : PageModel
    {
        private readonly ICourtService _courtService;
        private readonly IClubService _clubService;

        public CreateModel(ICourtService courtService, IClubService clubService)
        {
            _courtService = courtService;
            _clubService = clubService;
        }

        [BindProperty]
        public CourtRequest CourtRequest { get; set; }

        public SelectList StatusOptions { get; set; }
        public SelectList ClubOptions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            StatusOptions = new SelectList(Enum.GetValues(typeof(CourtStatus)).Cast<CourtStatus>());
            var clubs = await _clubService.GetAllClubsAsync();           
            ClubOptions = new SelectList(clubs, nameof(Club.Id), nameof(Club.Name));           
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                StatusOptions = new SelectList(Enum.GetValues(typeof(CourtStatus)));
                var clubs = await _clubService.GetAllClubsAsync();
                ClubOptions = new SelectList(clubs, nameof(Club.Id), nameof(Club.Name));

                return Page();
            }
            var allCourts = await _courtService.Get();
            if (allCourts != null && allCourts.Any(c => c.Num == CourtRequest.Num && c.ClubId == CourtRequest.ClubId))
            {
                ModelState.AddModelError("CourtRequest.Num", "Court number already exists in this club.");
                StatusOptions = new SelectList(Enum.GetValues(typeof(CourtStatus)));
                var clubs = await _clubService.GetAllClubsAsync();
                ClubOptions = new SelectList(clubs, nameof(Club.Id), nameof(Club.Name));

                return Page();
            }
            var court = new Court
            {
                Num = CourtRequest.Num,
                Status = CourtRequest.Status,
                ClubId = CourtRequest.ClubId
            };

            await _courtService.Create(court);

            return RedirectToPage("./Index");
        }
    }
}
