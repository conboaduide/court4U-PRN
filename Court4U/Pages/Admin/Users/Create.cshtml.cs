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
using BusinessLogic.Service;

namespace Court4U.Pages.Admin.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _iuserService;

        public CreateModel(IUserService iuserService)
        {
            _iuserService = iuserService;
        }
        [BindProperty]
        public DateOnly DOB { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [BindProperty]
        public User User { get; set; } = default!;
        public List<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
        public void OnGet()
        {
            Statuses = new List<SelectListItem>
        {
            new SelectListItem { Value = "Active", Text = "Active" },
            new SelectListItem { Value = "Inactive", Text = "Inactive" }
        };

            Roles = new List<SelectListItem>
        {
            new SelectListItem { Value = "Admin", Text = "Admin" },
            new SelectListItem { Value = "Member", Text = "Member" },
            new SelectListItem { Value = "Staff", Text = "Staff" },
            new SelectListItem { Value = "Owner", Text = "Owner" }
        };

            // Initialize User property if necessary
            User = new User();
        }

        public IActionResult OnPost()
        {

            // Handle form submission logic, e.g., save user to database
            User.DOB = DOB.ToDateTime(new TimeOnly());
            _iuserService.Create(User);

            return RedirectToPage("./Index");
        }
    }
}
