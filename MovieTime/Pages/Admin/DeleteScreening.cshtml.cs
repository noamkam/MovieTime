using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;

namespace MovieTime.Pages.Admin
{
    public class DeleteScreeningModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public DeleteScreeningModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        public List<Screening> Screenings { get; set; }

        public string Message { get; set; }

        public async Task OnGetAsync()
        {
            Screenings = await _context.Screenings
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .OrderBy(s => s.ScreeningDateTime)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var screening = await _context.Screenings
                .Include(s => s.Tickets)
                .FirstOrDefaultAsync(s => s.ScreeningId == id);

            if (screening == null)
            {
                return NotFound();
            }

            // מחיקת כל הכרטיסים שקשורים להקרנה
            _context.Tickets.RemoveRange(screening.Tickets);

            // מחיקת ההקרנה עצמה
            _context.Screenings.Remove(screening);

            await _context.SaveChangesAsync();
            Message = AdminMessages.DeleteScreeningSuccess;

            return RedirectToPage();
        }

    }
}
