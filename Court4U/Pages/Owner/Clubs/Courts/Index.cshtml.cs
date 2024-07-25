using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using Microsoft.IdentityModel.Tokens;
using BusinessLogic.Service.Interface;

namespace Court4U.Pages.Owner.Clubs.Courts
{
    public class IndexModel : PageModel
    {
        private ICourtService _courtService;

        public IndexModel(ICourtService courtService)
        {
            _courtService = courtService;
        }

        public IList<Court> Court { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var clubId = HttpContext.Session.GetString("ClubId");
            if (clubId.IsNullOrEmpty())
            {
                return RedirectToPage("/Owner/Clubs/Index");
            }

            Court = await _courtService.GetCourtsByClubIdAsync(clubId);
            return Page();
        }
    }
}
