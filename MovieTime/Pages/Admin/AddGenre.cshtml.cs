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
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            bool exists = _context.Genres.Any(g => g.Name.Trim().ToLower() == Genre.Name.Trim().ToLower());

            if (exists)
            {
                Message=  AdminMessages.GenreAlreadyExists;
                return Page();
            }

            _context.Genres.Add(Genre);
            await _context.SaveChangesAsync();

            Message = AdminMessages.AddGenreSuccess;
            ModelState.Clear();
            Genre = new Genre();

            return Page();
        }


    }
}
