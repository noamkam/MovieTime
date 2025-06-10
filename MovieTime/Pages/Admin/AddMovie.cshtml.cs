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

        public List<SelectListItem> GenresSelectList { get; set; } // ז'אנרים לבחירה
        public List<SelectListItem> LanguagesSelectList { get; set; } // שפות לבחירה

        public async Task OnGetAsync()
        {
            //מיון הז'אנרים והשפות לרשימות שמכילות טקסט להצגה
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
            // אם קיים כבר סרט כזה
            bool exists = _context.Movies.Any(m => m.Title == Movie.Title && m.GenreId == Movie.GenreId && m.LanguageId == Movie.LanguageId && m.DubbedIntoHebrew == Movie.DubbedIntoHebrew);
            if (exists)
            {
                Message = AdminMessages.MovieAlreadyExists;
                return Page();
            }
            _context.Movies.Add(Movie);
            await _context.SaveChangesAsync();
            Message = AdminMessages.AddMovieSuccess;
            return RedirectToPage("AddMovie"); 
        }
    }
}
