using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Court4U.Pages.Shared
{
    public class _UserLayoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public _UserLayoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Username { get; private set; }
        public void OnGet()
        {
            CheckSession();
        }

        private void CheckSession()
        {
            Username = _httpContextAccessor.HttpContext.Session.GetString("Username");
        }
    }
}
