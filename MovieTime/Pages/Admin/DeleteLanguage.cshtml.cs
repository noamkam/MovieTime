using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTime.Context;
using MovieTime.Models;
using Microsoft.EntityFrameworkCore;
using MovieTime.Messages;

namespace MovieTime.Pages.Admin
{
    public class DeleteLanguageModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public DeleteLanguageModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int? SelectedLanguageId { get; set; }

        public List<SelectListItem> LanguageOptions { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            //רשימה של כל השפות ממסד הנתונים
            LanguageOptions = _context.Languages
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //מחפש את השפה שנבחרה 
            var language = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == SelectedLanguageId);

            if (language == null)
            {
                Message = AdminMessages.LanguageNull;
                OnGet();
                return Page();
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();

            Message = AdminMessages.DeleteLanguageSuccess; 
            OnGet(); // ריענון הרשימה לאחר המחיקה
            return Page();
        }
    }
}
