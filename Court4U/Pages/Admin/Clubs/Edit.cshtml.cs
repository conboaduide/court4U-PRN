﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Repository.Request;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.SignalR;

namespace Court4U.Pages.Admin.Clubs
{
    public class EditModel : PageModel
    {
        private readonly IClubService _clubService;
        private IHubContext<ClubHub> _clubHub;

        [BindProperty]
        public ClubRequest ClubRequest { get; set; }

        public EditModel(IClubService clubService, IHubContext<ClubHub> clubHub)
        {
            _clubService = clubService;
            _clubHub = clubHub;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var club = await _clubService.GetClubByIdAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            ClubRequest = new ClubRequest
            {
                Name = club.Name,
                Description = club.Description,
                Address = club.Address,
                CityOfProvince = club.CityOfProvince,
                District = club.District,
                LogoUrl = club.LogoUrl,
                UserId = club.UserId
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var club = await _clubService.GetClubByIdAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            club.Name = ClubRequest.Name;
            club.Description = ClubRequest.Description;
            club.Address = ClubRequest.Address;
            club.CityOfProvince = ClubRequest.CityOfProvince;
            club.District = ClubRequest.District;
            club.LogoUrl = ClubRequest.LogoUrl;
            club.UserId = ClubRequest.UserId;

            await _clubService.UpdateClubAsync(club);

            await _clubHub.Clients.All.SendAsync("ClubChanged");

            return RedirectToPage("Index");
        }
    }
}
