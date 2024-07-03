using BusinessLogic.Service.Interface;
using DataAccess.Entity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static DataAccess.Entity.Enums;

namespace Court4U.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public RegisterModel(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "User Name")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            [Display(Name = "Confirm Password")]
            public string ConfirmPassword { get; set; }
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

            var existingUser = await _userService.GetByUsernameAndEmail(Input.Username, Input.Email);
            if (existingUser != null)
            {
                if (existingUser.Username.Equals(Input.Username, StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Input.Username", "Username already exists.");
                }
                if (existingUser.Email.Equals(Input.Email, StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Input.Email", "Email already exists.");
                }
                return Page();
            }
            var newUser = new User
            {
                Username = Input.Username,
                Email = Input.Email,
                Phone = Input.Phone,
                Password = Input.Password,
                Status = Status.Inactive,
                Role = Roles.Member,
            };
            var result = await _userService.Create(newUser);

            string token = Guid.NewGuid().ToString();

            existingUser = await _userService.GetByUsernameAndEmail(Input.Username, Input.Email);
            existingUser.Token = token;
            await _userService.Update(existingUser);
            await _emailService.SendEmail(newUser.Email, token);

            if (result != null) 
            {
                return RedirectToPage("/Login");
            }
            else 
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the account. Please try again.");
                return Page();
            }
        }
    }
}
