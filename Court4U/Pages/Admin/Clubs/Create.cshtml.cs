using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Entity.Data;
using System.Threading.Tasks;
using BusinessLogic.Service.Interface;
using DataAccess.Repository.Request;

namespace Court4U.Pages.Admin.Clubs
{
    public class CreateModel : PageModel
    {
        private readonly IClubService _clubService;

        public CreateModel(IClubService clubService)
        {
            _clubService = clubService;
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

            return RedirectToPage("Index");
        }
    }
}