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
using BusinessLogic.Service;

namespace Court4U.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService= userService;
        }

        public IList<User> Users { get; private set; }
        public string CurrentFilter { get; set; }

        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            var userRole = HttpContext.Session.GetString("Role");
            if (userRole == null || userRole != "Admin")
            {
                return RedirectToPage("/Index");
            }

            CurrentFilter = searchString;

            var users = await _userService.Get();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(c => c.Username.ToString().Contains(searchString)).ToList();
            }

            Users = users;
            return Page();
        }
    }
}
