using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;

namespace MovieTime.Pages.Admin
{
    public class AddHallModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public AddHallModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Hall Hall { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //    return Page();

            // הוספת האולם
            _context.Halls.Add(Hall);
            await _context.SaveChangesAsync(); //שומר את האולם במסד הנתונים  

            //  יצירת כיסאות לאולם
         
            for (int i = 1; i <= Hall.SeatCount; i++)
            {
                _context.Seats.Add(new Seat
                {
                    HallId = Hall.HallId,
                    SeatNumber = i
                });
            }

            await _context.SaveChangesAsync(); //שמירה של הכיסאות במסד הנתונים

            Message = AdminMessages.AddHallSuccess; // הודעת הצלחה
            return Page();
        }

        public void OnGet()
        {
        }
    }
}
