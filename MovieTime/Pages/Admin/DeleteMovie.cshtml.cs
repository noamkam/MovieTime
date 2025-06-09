using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;

namespace MovieTime.Pages.Admin
{
    public class DeleteMovieModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public DeleteMovieModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        public List<Movie> Movies { get; set; }

        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            Movies = await _context.Movies
                .OrderBy(m => m.Title)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Screenings)
                    .ThenInclude(s => s.Tickets)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            // מחיקת כל הכרטיסים שקשורים להקרנות של הסרט
            foreach (var screening in movie.Screenings)
            {
                _context.Tickets.RemoveRange(screening.Tickets);
            }

            // מחיקת כל ההקרנות של הסרט
            _context.Screenings.RemoveRange(movie.Screenings);

            // מחיקת הסרט עצמו
            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            Message = AdminMessages.DeleteMovieSuccess;
            return RedirectToPage();
        }

    }
}
