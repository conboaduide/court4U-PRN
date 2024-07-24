using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages.Owner.Clubs
{
    public class IndexModel : PageModel
    {
        private readonly IClubService clubService;
        [BindProperty]
        public string ClubId { get; set; }
        public IndexModel(IClubService iClubService)
        {
            this.clubService = iClubService;
        }
        public IList<Club> Clubs { get; set; } = default!;   
        public async Task OnGetAsync()
        {
            var userId = HttpContext.Session.Get("UserId");
            var convertUserId = System.Text.Encoding.UTF8.GetString(userId);
            Clubs = await clubService.GetClubByUserIdAsync(convertUserId);
            Console.WriteLine(Clubs);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            HttpContext.Session.SetString("ClubId", ClubId);
            
            return Redirect("/Owner/Dashboard");
        }
    }
}
