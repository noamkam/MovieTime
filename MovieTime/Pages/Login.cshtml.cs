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
        public void OnGet()
        {
            // Nothing on GET
        }

        public async Task<IActionResult> OnPost()
        {
            //Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            var customer = await _context.Customers
            .Where(c => c.UserName == Username && c.Password == Password)
            .FirstOrDefaultAsync();
            //Check if customer exists with the username and password
            if (customer != null)
            {
                return RedirectToPage("/Index");
            }

            ErrorMessage = LoginMessages.InvalidCredentials;
            return Page();
        }
    }
}
