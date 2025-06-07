using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;

namespace MovieTime.Pages.Admin
{
    public class AddMovieModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public AddMovieModel(MovieTimeDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Movie Movie { get; set; }

        public string Message { get; set; }

        public List<SelectListItem> GenresSelectList { get; set; }
        public List<SelectListItem> LanguagesSelectList { get; set; }

        public async Task OnGetAsync()
        {
            GenresSelectList = await _context.Genres
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                })
                .OrderBy(g => g.Text)
                .ToListAsync();

            LanguagesSelectList = await _context.Languages
                .Select(l => new SelectListItem
                {
                    Value = l.Id.ToString(),
                    Text = l.Name
                })
                .OrderBy(l => l.Text)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var movie = new Movie
            {
                Title  = Movie.Title,
                Description = Movie.Description,
                GenreId = Movie.GenreId,
                ReleaseDate = Movie.ReleaseDate,
                Image = Movie.Image,
                Duration = Movie.Duration,
                LanguageId = Movie.LanguageId,
                DubbedIntoHebrew = Movie.DubbedIntoHebrew
            };

            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();
            Message = AdminMessages.AddMovieSuccess;
            return RedirectToPage("AddMovie"); 
        }
    }
}
