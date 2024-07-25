using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Court4U.Pages.Owner.Clubs.Courts
{
    public class DeleteModel : PageModel
    {
        private ICourtService _courtService;
        private IHubContext<ClubHub> _hub;

        public DeleteModel(ICourtService courtService, IHubContext<ClubHub> hub)
        {
            _courtService = courtService;
            _hub = hub;
        }

        [BindProperty]
        public Court Court { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _courtService.Get(id);

            if (court == null)
            {
                return NotFound();
            }
            else
            {
                Court = court;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _courtService.Get(id);
            if (court != null)
            {
                Court = court;
                await _courtService.Delete(id);

                await _hub.Clients.All.SendAsync("CourtChanged");
            }

            return RedirectToPage("./Index");
        }
    }
}
