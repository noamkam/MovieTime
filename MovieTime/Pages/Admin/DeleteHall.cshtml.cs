using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTime.Context;
using MovieTime.Models;
using Microsoft.EntityFrameworkCore;
using MovieTime.Messages;

namespace MovieTime.Pages.Admin
{
    public class DeleteHallModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public DeleteHallModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int SelectedHallId { get; set; }

        public List<SelectListItem> HallOptions { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            HallOptions = _context.Halls
                .Select(h => new SelectListItem
                {
                    Value = h.HallId.ToString(),
                    Text = $"אולם {h.HallId} - {h.SeatCount} כיסאות" + (h.IsVIP ? " (VIP)" : "")
                }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var hall = await _context.Halls
                .Include(h => h.Seats)
                .FirstOrDefaultAsync(h => h.HallId == SelectedHallId);

            if (hall == null)
            {
                ModelState.AddModelError(string.Empty, "האולם לא נמצא.");
                OnGet(); 
                return Page();
            }

            // מחיקת הכיסאות
            _context.Seats.RemoveRange(hall.Seats);

            // מחיקת האולם
            _context.Halls.Remove(hall);

            await _context.SaveChangesAsync();

            Message= AdminMessages.DeleteHallSuccess; // הודעת הצלחה

            // לרענון הרשימה לאחר מחיקה
            OnGet();
            return Page();
        }
    }
}
