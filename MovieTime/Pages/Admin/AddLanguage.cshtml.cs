using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;

namespace MovieTime.Pages.Admin
{
    public class AddLanguageModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public AddLanguageModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Language Language { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            if (Language == null ) // אם לא הוזנה שפה
            {
                Message = AdminMessages.LanguageNull;
                return Page();
            }
            //אם השפה כבר קיימת במסד
            bool exists = _context.Languages.Any(l => l.Name.Trim() == Language.Name.Trim());
            if (exists)
            {
                Message = AdminMessages.LanguageAlreadyExists;
                return Page();
            }
            _context.Languages.Add(Language);
            await _context.SaveChangesAsync();
            Message = AdminMessages.AddLanguageSuccess;
            return Page();
        }
    }
}
