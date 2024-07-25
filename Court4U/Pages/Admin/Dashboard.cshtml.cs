using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Court4U.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IClubService _clubService;
        private readonly ICourtService _courtService;

        public int MemberCount { get; set; }
        public int CourtOwnerCount { get; set; }
        public int ClubCount { get; set; }
        public int CourtCount { get; set; }
        public Dictionary<string, int[]> MonthlyData { get; set; }

        public DashboardModel(IUserService userService, IClubService clubService, ICourtService courtService)
        {
            _userService = userService;
            _clubService = clubService;
            _courtService = courtService;
            MonthlyData = new Dictionary<string, int[]>();
        }

        public async Task OnGetAsync(int? year)
        {
            // Set default year if none is provided
            var selectedYear = year ?? DateTime.Now.Year;

            // Fetch statistics based on the selected year
            MemberCount = await GetMemberCount(selectedYear);
            CourtOwnerCount = await GetCourtOwnerCount(selectedYear);
            ClubCount = await GetClubCount(selectedYear);
            CourtCount = await GetCourtCount(selectedYear);
            MonthlyData = await GetMonthlyData(selectedYear);
        }

        private async Task<int> GetMemberCount(int year)
        {
            var users = await _userService.Get();
            return users.Where(u => u.Role == DataAccess.Entity.Enums.Roles.Member).Count();
        }

        private async Task<int> GetCourtOwnerCount(int year)
        {
            var users = await _userService.Get();
            return users.Where(u => u.Role == DataAccess.Entity.Enums.Roles.Owner).Count();
        }

        private async Task<int> GetClubCount(int year)
        {
            var clubs = await _clubService.GetAllClubsAsync();
            return clubs.Count();
        }

        private async Task<int> GetCourtCount(int year)
        {
            var courts = await _courtService.Get();
            return courts.Count();
        }

        private async Task<Dictionary<string, int[]>> GetMonthlyData(int year)
        {
            var membersMonthly = await _userService.GetMonthlyCounts(year, DataAccess.Entity.Enums.Roles.Member);
            var ownersMonthly = await _userService.GetMonthlyCounts(year, DataAccess.Entity.Enums.Roles.Owner);
            var clubsMonthly = await _clubService.GetMonthlyCounts(year);
            var courtsMonthly = await _courtService.GetMonthlyCounts(year);

            return new Dictionary<string, int[]>
            {
                { "Members", membersMonthly },
                { "CourtOwners", ownersMonthly },
                { "Clubs", clubsMonthly },
                { "Courts", courtsMonthly }
            };
        }
    }
}
