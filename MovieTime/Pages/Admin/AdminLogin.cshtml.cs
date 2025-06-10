using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Pages
{
    public class AdminLoginModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public AdminLoginModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = LoginMessages.UsernameRequired)]
        public string UserName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = LoginMessages.PasswordRequired)]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            var admin = _context.Admins.FirstOrDefault(a => a.UserName == UserName && a.Password == Password);
            if (admin != null)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                HttpContext.Session.SetString("UserName", admin.Name);
                return RedirectToPage("/Admin/SiteManagement");
            }

            ErrorMessage = LoginMessages.InvalidCredentials;
            return Page();
        }
    }
}
