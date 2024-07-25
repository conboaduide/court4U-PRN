using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using static DataAccess.Entity.Enums;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Court4U.Pages.Owner.Clubs.Courts
{
    public class EditModel : PageModel
    {
        private ICourtService _courtService;
        private IHubContext<ClubHub> _hub;

        public EditModel(ICourtService courtService, IHubContext<ClubHub> hub)
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
            Court = court;
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(CourtStatus)).Cast<CourtStatus>().Select(s => new SelectListItem
            {
                Value = s.ToString(),
                Text = s.ToString()
            }).ToList(), "Value", "Text");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            await _courtService.Update(Court);

            await _hub.Clients.All.SendAsync("CourtChanged");

            return RedirectToPage("./Index");
        }
    }
}
