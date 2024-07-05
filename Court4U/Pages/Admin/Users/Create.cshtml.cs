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

namespace Court4U.Pages.Admin.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserService _iuserService;

        public CreateModel(IUserService iuserService)
        {
            _iuserService = iuserService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            await _iuserService.Create(User);

            return RedirectToPage("./Index");
        }
    }
}
