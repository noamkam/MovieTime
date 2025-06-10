 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;

namespace MovieTime.Pages.Admin
{
    public class AddScreeningModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public AddScreeningModel(MovieTimeDBContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Screening Screening { get; set; }

        public string Message { get; set; }

        public List<SelectListItem> HallsSelectList { get; set; }

        public List<SelectListItem> MoviesSelectList { get; set; }

        public async Task OnGetAsync()
        {
            HallsSelectList = await _context.Halls
                .Select(h => new SelectListItem
                {
                    Value = h.HallId.ToString(),
                    Text = h.HallId.ToString()

                })
                .OrderBy(h => h.Text)
                .ToListAsync();

            MoviesSelectList = await _context.Movies
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Title

                })
                .OrderBy(h => h.Text)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            bool exists = _context.Screenings.Any(s => s.MovieId == Screening.MovieId && s.HallId == Screening.HallId && s.ScreeningDateTime == Screening.ScreeningDateTime);
            if (exists)
            {
                Message = AdminMessages.ScreeningAlreadyExists;
                return Page();
            }
           
            if (Screening.MovieId == 0 || Screening.HallId == 0)
            {
                Message = AdminMessages.ScreeningNull;
                return Page();
            }
            _context.Screenings.Add(Screening);
            await _context.SaveChangesAsync();
            Message = AdminMessages.AddScreeningSuccess;
            return RedirectToPage("AddScreening");
           
        }
    }
}
