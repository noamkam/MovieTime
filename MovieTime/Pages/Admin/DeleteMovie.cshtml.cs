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
            //טעינת רשימת הסרטים
            Movies = await _context.Movies
                .OrderBy(m => m.Title)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            //בדיקה אם הסרט קיים במסד
            var movie = await _context.Movies
                .Include(m => m.Screenings)
                    .ThenInclude(s => s.Tickets)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            // מחיקת הכרטיסים    
            foreach (var screening in movie.Screenings)
            {
                _context.Tickets.RemoveRange(screening.Tickets);
            }

            // מחיקת ההקרנות  
            _context.Screenings.RemoveRange(movie.Screenings);

            // מחיקת הסרט 
            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            Message = AdminMessages.DeleteMovieSuccess;
            return RedirectToPage();
        }

    }
}
