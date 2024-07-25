using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Court4U.Pages.Owner.Clubs.Courts
{
    public class CreateModel : PageModel
    {
        private ICourtService _courtService;
        private IHubContext<ClubHub> _hub;

        public CreateModel(ICourtService courtService, IHubContext<ClubHub> hub)
        {
            _courtService = courtService;
            _hub = hub;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Court Court { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Court.Status = Enums.CourtStatus.Active;

            await _courtService.Create(Court);

            await _hub.Clients.All.SendAsync("CourtChanged");

            return RedirectToPage("./Index");
        }
    }
}
