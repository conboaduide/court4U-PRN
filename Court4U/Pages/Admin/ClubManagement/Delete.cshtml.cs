using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Entity.Data;
using System.Threading.Tasks;
using BusinessLogic.Service.Interface;

namespace Court4U.Pages.Admin.ClubManagement
{
    public class DeleteModel : PageModel
    {
        private readonly IClubService _clubService;

        public DeleteModel(IClubService clubService)
        {
            _clubService = clubService;
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

            return RedirectToPage("Index");
        }
    }
}
