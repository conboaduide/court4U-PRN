using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Entity.Data;
using System.Threading.Tasks;
using BusinessLogic.Service.Interface;
using DataAccess.Repository.Request;
using Microsoft.AspNetCore.SignalR;

namespace Court4U.Pages.Admin.Clubs
{
    public class CreateModel : PageModel
    {
        private readonly IClubService _clubService;
        private IHubContext<ClubHub> _clubHub;

        public CreateModel(IClubService clubService, IHubContext<ClubHub> clubHub)
        {
            _clubService = clubService;
            _clubHub = clubHub;
        }

        [BindProperty]
        public ClubRequest Club { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _clubService.AddClubAsync(Club);

            await _clubHub.Clients.All.SendAsync("ClubChanged");

            return RedirectToPage("Index");
        }
    }
}