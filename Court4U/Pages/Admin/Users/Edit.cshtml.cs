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
using System.Data;

namespace Court4U.Pages.Admin.Users
{
    public class EditModel : PageModel
    {
        private readonly IUserService _iuserService;

        public EditModel(IUserService iuserService)
        {
            _iuserService = iuserService;
        }

        [BindProperty]
        public User User { get; set; }

        public List<SelectListItem> Statuses { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpContext.Session.SetString("UserId", id);

            var user = await _iuserService.Get(id);
            if (user == null)
            {
                return NotFound();
            }

            User = user;

            // Initialize Statuses and Roles
            Statuses = Enum.GetValues(typeof(Status))
                           .Cast<Status>()
                           .Select(s => new SelectListItem
                           {
                               Value = s.ToString(),
                               Text = s.ToString()
                           }).ToList();

            Roles = Enum.GetValues(typeof(Roles))
                        .Cast<Roles>()
                        .Select(r => new SelectListItem
                        {
                            Value = r.ToString(),
                            Text = r.ToString()
                        }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var id = HttpContext.Session.GetString("EditUserId");
            if (id == null)
            {
                return NotFound();
            }

            User.Id = id;

            try
            {
                await _iuserService.Update(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(string id)
        {
            return _iuserService.UserExists(id);
        }
    }

}
