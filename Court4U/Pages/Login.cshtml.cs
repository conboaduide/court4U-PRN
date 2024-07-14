using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Service.Interface;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static DataAccess.Entity.Enums;

namespace Court4U.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userService.CheckLogin(Input.Username, Input.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Email", user.Email);
                HttpContext.Session.SetString("Role", user.Role.ToString());

                if (user.Role == Roles.Admin)
                {
                    return RedirectToPage("/Admin/Dashboard");
                }
                if (user.Role == Roles.Owner)
                {
                    return RedirectToPage("/Owner/MemberSubscription/Index");
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Username or Password.");
            return Page();
        }
    }
}
