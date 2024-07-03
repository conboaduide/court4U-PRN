using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Service.Interface;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
            [EmailAddress]
            public string Email { get; set; }

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

            var user = await _userService.CheckLogin(Input.Email, Input.Password);

            if (user != null)
            {
                return RedirectToPage("/Index");  
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
