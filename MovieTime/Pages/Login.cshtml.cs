using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieTime.Context;
using MovieTime.Messages;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Pages
{
    public class LoginModel : PageModel
    {
        public MovieTimeDBContext _context;

        [BindProperty]
        [Required(ErrorMessage = LoginMessages.UsernameRequired)]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = LoginMessages.PasswordRequired)]
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public LoginModel(MovieTimeDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customer = await _context.Customers
            .Where(c => c.UserName == Username && c.Password == Password)
            .FirstOrDefaultAsync();
            // בדיקה אם יש משתמש עם הסיסמא והשם שהוזמנו
            if (customer != null)
            {
                HttpContext.Session.SetString("UserName", customer.Name);
                HttpContext.Session.SetString("CustomerId", customer.Id.ToString());
                return RedirectToPage("/Index");
            }

            ErrorMessage = LoginMessages.InvalidCredentials;
            return Page();
        }
    }
}
