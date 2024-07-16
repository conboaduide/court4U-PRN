using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Court4U.Pages.Admin.Courts
{
    public class IndexModel : PageModel
    {
        private readonly ICourtService _courtService;

        public IndexModel(ICourtService courtService)
        {
            _courtService = courtService;
        }
        public IList<Court> Courts { get; private set; }
        public string CurrentFilter { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == null || userRole != "Admin")
            {
                return RedirectToPage("/Index");
            }

            CurrentFilter = searchString;

            var courts = await _courtService.Get();
            courts = courts.OrderBy(c => c.Club.Name)
                                .ThenBy(c => c.Club.CityOfProvince)
                                .ThenBy(c => c.Num)
                                .ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                courts = courts.Where(c => c.Num.ToString().Contains(searchString) ||
                                           c.Status.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                           (c.Club != null && c.Club.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                                           (c.Club != null && c.Club.CityOfProvince.Contains(searchString, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            Courts = courts;
            return Page();
        }
    }
}
