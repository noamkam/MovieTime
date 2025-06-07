using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;

namespace MovieTime.Pages.Admin
{
    public class AddGenreModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public AddGenreModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Genre Genre { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            _context.Genres.Add(Genre);
            await _context.SaveChangesAsync();

            Message = AdminMessages.AddGenreSuccess;
            return Page();
        }
    }
}
