using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        IUserService _userService;
        IClubService _clubService;
        ICourtService _courtService;

        public int MemberCount { get; set; }
        public int CourtOwnerCount { get; set; }
        public int ClubCount { get; set; }
        public int CourtCount { get; set; }

        public DashboardModel(IUserService userService, IClubService clubService, ICourtService courtService) 
        { 
            _userService = userService;
            _clubService = clubService;
            _courtService = courtService;
        }

        public async Task OnGet()
        {
            // Replace with actual data fetching logic
            MemberCount = await GetMemberCount();
            CourtOwnerCount = await GetCourtOwnerCount();
            ClubCount = await GetClubCount();
            CourtCount = await GetCourtCount();
        }

        private async Task<int> GetMemberCount()
        {
            var users = await _userService.Get();
            return users.Where(u => u.Role == DataAccess.Entity.Enums.Roles.Member).Count();
        }

        private async Task<int> GetCourtOwnerCount()
        {
            var users = await _userService.Get();
            return users.Where(u => u.Role == DataAccess.Entity.Enums.Roles.Owner).Count();
        }

        private async Task<int> GetClubCount()
        {
            var clubs = await _clubService.GetAllClubsAsync();
            return clubs.Count();
        }

        private async Task<int> GetCourtCount()
        {
            var courts = await _courtService.Get();
            return courts.Count();
        }
    }
}
