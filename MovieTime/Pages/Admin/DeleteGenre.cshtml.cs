using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;

namespace MovieTime.Pages.Admin
{
    public class DeleteGenreModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public DeleteGenreModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        public List<Genre> Genres { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            Genres = await _context.Genres.ToListAsync(); // טעינת הז'אנרים ממסד הנתונים
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        { 
            // ID-מחפש במסד את הז'אנר שהוקלד לפי ה
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                await _context.SaveChangesAsync();
                Message = AdminMessages.DeleteGenreSuccess;
            }
            return RedirectToPage();
        }
    }
}
