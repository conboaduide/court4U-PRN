﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Entity.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;


namespace Court4U.Pages.Admin.Clubs
{
    public class IndexModel : PageModel
    {
        private readonly IClubService _clubService;

        public IndexModel(IClubService clubService)
        {
            _clubService = clubService;
        }

        public IList<Club> Clubs { get; private set; }
        public string CurrentFilter { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == null || userRole != "Admin")
            {
                return RedirectToPage("/Index");
            }
            CurrentFilter = searchString;

            var clubs = await _clubService.GetAllClubsAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                clubs = clubs.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                         c.District.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                         c.CityOfProvince.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            Clubs = (IList<Club>)clubs;
            return Page();
        }
    }
}
