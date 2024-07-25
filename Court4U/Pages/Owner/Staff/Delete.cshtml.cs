using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entity;
using DataAccess.Entity.Data;
using DataAccess.Repository.Interface;
using BusinessLogic.Service.Interface;
using BusinessLogic.Service;

namespace Court4U.Pages.Owner.Staff
{
    public class DeleteModel : PageModel
    {
        private readonly DataAccess.Entity.Court4UDbContext _context;
        private readonly IStaffProfileService _staffProfileService;

        public DeleteModel(DataAccess.Entity.Court4UDbContext context, IStaffProfileService staffProfileService)
        {
            _context = context;
            _staffProfileService = staffProfileService;
        }

        [BindProperty]
        public StaffProfile StaffProfile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string toilaid)
        {
            StaffProfile = await _staffProfileService.Get(toilaid);
            _staffProfileService.Delete(StaffProfile.Id);


            return RedirectToPage("./Index");
        }
    }
}
