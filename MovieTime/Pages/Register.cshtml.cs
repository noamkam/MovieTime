using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

public class RegisterModel : PageModel
{
    private readonly MovieTimeDBContext _context;

    public RegisterModel(MovieTimeDBContext context)
    {
        _context = context;
    }
    [BindProperty]
    public string Name { get; set; }
    [BindProperty]
    public string PhoneNumber { get; set; }
    [BindProperty]
    public DateTime DateOfBirth { get; set; }
    [BindProperty]
 
    public string Email { get; set; }
    [BindProperty]
    public string Password { get; set; }
    [BindProperty]
    public string UserName { get; set; }
    [BindProperty]
    public string ErrorMessage { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        // בדיקה אם השדות ריקים
        if (string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(PhoneNumber) ||
            DateOfBirth == default || 
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Password) ||
            string.IsNullOrWhiteSpace(UserName))
        {
            ErrorMessage = ErrorMessages.EmptyFields;
            return Page();
        }
        if (PhoneNumber.Length != 10 || !PhoneNumber.All(char.IsDigit))//בדיקה שהטלפון 10 ספרות
        {
            ErrorMessage = ErrorMessages.PhoneInvalid;
            return Page();
        }
        var customer = new Customer
        {
            UserName = UserName,
            Email = Email,
            Password = Password,
            DateOfBirth = DateOfBirth,
            Name = Name,
            PhoneNumber = PhoneNumber
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }

}
