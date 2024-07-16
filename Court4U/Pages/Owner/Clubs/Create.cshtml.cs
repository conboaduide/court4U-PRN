using BusinessLogic.Service.Interface;
using DataAccess.Repository.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages.Owner.Clubs
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
            var userId = HttpContext.Session.Get("UserId");
            var convertUserId = System.Text.Encoding.UTF8.GetString(userId);
            Club.UserId = convertUserId;
            await _clubService.AddClubAsync(Club);

            return RedirectToPage("Index");
        }
    }
}
