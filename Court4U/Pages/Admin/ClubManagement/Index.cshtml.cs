using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Entity.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Interface;


namespace Court4U.Pages.Admin.ClubManagement
{
    public class IndexModel : PageModel
    {
        private readonly IClubService _clubService;

        public IndexModel(IClubService clubService)
        {
            _clubService = clubService;
        }

        public IList<Club> Clubs { get; private set; }

        public async Task OnGetAsync()
        {
            Clubs = (IList<Club>)await _clubService.GetAllClubsAsync();
        }
    }
}
