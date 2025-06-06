using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieTime.Context;
using MovieTime.Models;

namespace MovieTime.Pages
{
    public class MoviesModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public MoviesModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        public List<Movie> Movies { get; set; }

        public async Task OnGetAsync()
        {
            Movies = await _context.Movies
           .Include(m => m.Genre) // Include Genre info
           .ToListAsync();
        }
    }

}
