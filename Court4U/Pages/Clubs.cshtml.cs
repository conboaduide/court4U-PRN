using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Court4U.Pages
{
    public class ClubsModel : PageModel
    {
        private readonly IClubService _clubService;

        public ClubsModel(IClubService clubService)
        {
            _clubService = clubService;
        }

        public List<Club> Clubs { get; set; }

        public async Task OnGetAsync()
        {
            Clubs = (List<Club>)await _clubService.GetAllClubsAsync();
        }
    }
}
