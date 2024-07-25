using BusinessLogic.Service;
using BusinessLogic.Service.Interface;
using DataAccess.Repository.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages.Owner.Clubs
{
    public class CreateModel : PageModel
    {
        private readonly IClubService _clubService;
        private ICloudinaryService _cloudinaryService;

        public CreateModel(IClubService clubService, ICloudinaryService cloudinaryService)
        {
            _clubService = clubService;
            _cloudinaryService = cloudinaryService;
        }

        [BindProperty]
        public ClubRequest Club { get; set; }

        [BindProperty]
        public IFormFile LogoFile { get; set; }


        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = HttpContext.Session.Get("UserId");
            var convertUserId = System.Text.Encoding.UTF8.GetString(userId);
            Club.UserId = convertUserId;

            if (LogoFile != null && LogoFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await LogoFile.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    var imageUrl = _cloudinaryService.UploadImage(imageBytes);
                    Club.LogoUrl = imageUrl;
                    await _clubService.AddClubAsync(Club);
                }
            }
            else
            {
                // Handle the case where no file was uploaded
                ModelState.AddModelError("LogoFile", "Please upload a file.");
            }

            return RedirectToPage("Index");
        }

        public void OnPostUploadImage(byte[] imageBytes)
        {
            var imageUrl = _cloudinaryService.UploadImage(imageBytes);
            // Use the imageUrl as needed
        }
    }
}
