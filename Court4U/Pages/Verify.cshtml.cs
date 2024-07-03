using BusinessLogic.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages
{
    public class VerifyModel : PageModel
    {
        private readonly IUserService iuserService;

        public VerifyModel(IUserService userService)
        {
            iuserService = userService;
        }

        public async Task<IActionResult> OnGetAsync(string token)
        {
            bool result = await iuserService.CheckVerify(token);

            if (result)
            {
                // thanh cong
                ModelState.AddModelError(string.Empty, "Please check your eamil.");
                return RedirectToPage("/Login");
            }
            else
            {
                // that bai
                return RedirectToPage("/Error");
            }
        }
    }
}
