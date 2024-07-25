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
using Microsoft.EntityFrameworkCore;

namespace Court4U.Pages.Owner.Staff
{
    public class CreateModel : PageModel
    {
        private readonly IUserService userService;
        private readonly IStaffProfileService staffProfileService;

        [BindProperty]
        public StaffProfile StaffProfile { get; set; } = default!;
        [BindProperty]
        public User User { get; set; } = default!;
        [BindProperty]
        public DateOnly DOB { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [BindProperty(SupportsGet = true)]
        public string Gender { get; set; }

        public CreateModel(IUserService userService, IStaffProfileService staffProfileService)
        {
            this.userService = userService;
            this.staffProfileService = staffProfileService;
        }

        public IActionResult OnGet()
        {
            var clubId = HttpContext.Session.GetString("ClubId");
            if (string.IsNullOrEmpty(clubId))
            {
                return RedirectToPage("/Owner/Clubs/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var clubId = HttpContext.Session.GetString("ClubId");
            if (string.IsNullOrEmpty(clubId))
            {
                return RedirectToPage("/Owner/Clubs/Index");
            }

            var existingUser = await userService.GetByUsernameAndEmail(User.Username, User.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Username or Email is already taken.");
                return Page();
            }

            User.Status = Enums.Status.Active;
            User.Role = Enums.Roles.Staff;
            User.CreatedDate = DateTime.Now;
            User.UpdatedDate = DateTime.Now;
            
            if (HttpContext.Session.GetString("ClubId") != null)
            {
                StaffProfile.ClubId = HttpContext.Session.GetString("ClubId");
                StaffProfile.CreatedDate = DateTime.Now;
                StaffProfile.UpdatedDate = DateTime.Now;
                StaffProfile.Id = Guid.NewGuid().ToString();
                StaffProfile.UserId = User.Id;
                StaffProfile.User = User;
            }
            else
            {
                return RedirectToPage("/Owner/Clubs/Index");
            }
            await staffProfileService.Create(StaffProfile);
            

            return RedirectToPage("./Index");
        }
    }
}