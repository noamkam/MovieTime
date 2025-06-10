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
        // בדיקה אם אחד מהשדות ריק
        if (string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(PhoneNumber) ||
            DateOfBirth == default || // תאריך לידה לא נבחר
            string.IsNullOrWhiteSpace(Email) ||
            string.IsNullOrWhiteSpace(Password) ||
            string.IsNullOrWhiteSpace(UserName))
        {
            ErrorMessage = ErrorMessages.EmptyFields;
            return Page(); // נשארים באותו עמוד ומציגים את ההודעה
        }
        if (PhoneNumber.Length != 10 || !PhoneNumber.All(char.IsDigit))
        {
            ErrorMessage = ErrorMessages.PhoneInvalid;
            return Page();
        }

        // אם הכל תקין - ממשיכים לשמור במסד
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
