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
            _context.Languages.Add(Language);
            await _context.SaveChangesAsync();

            Message = AdminMessages.AddLanguageSuccess;
            return Page();
        }
    }
}
