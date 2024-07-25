using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Entity.Data;
using System.Threading.Tasks;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Court4U.Pages.Admin.Clubs
{
    public class DeleteModel : PageModel
    {
        private readonly IClubService _clubService;
        private IHubContext<ClubHub> _clubHub;

        public DeleteModel(IClubService clubService, IHubContext<ClubHub> clubhub)
        {
            _clubService = clubService;
            _clubHub = clubhub;
        }

        [BindProperty]
        public Club Club { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Club = await _clubService.GetClubByIdAsync(id);

            if (Club == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            await _clubService.DeleteClubAsync(id);

            await _clubHub.Clients.All.SendAsync("ClubChanged");

            return RedirectToPage("Index");
        }
    }
}
