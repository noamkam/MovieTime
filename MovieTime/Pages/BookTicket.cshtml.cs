using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTime.Context;
using MovieTime.Messages;

namespace MovieTime.Pages
{
    public class BookTicketModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public BookTicketModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public int SelectedMovieId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SelectedScreeningId { get; set; }

        [BindProperty(SupportsGet = true)]
        
        public int NumTickets { get; set; }

        public List<SelectListItem> MoviesSelectList { get; set; } = new();
        public List<SelectListItem> ScreeningsList { get; set; } = new();

        public string? Message { get; set; }

        public void OnGet()
        {
            MoviesSelectList = _context.Movies
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Title
                }).ToList();
        }

        public async Task<JsonResult> OnGetScreeningsAsync(int movieId)
        {
            var screenings = await _context.Screenings
                .Where(s => s.MovieId == movieId)
                .Select(s => new
                {
                    id = s.ScreeningId,
                    date = s.ScreeningDateTime.ToString("dd/MM/yyyy HH:mm")
                }).ToListAsync();

            return new JsonResult(screenings);
        }

        public IActionResult OnPost()
        {
            if (SelectedMovieId == 0)
            {
                Message = ErrorMessages.MovieNotSelected;
                OnGet(); // כדי למלא את MoviesSelectList מחדש
                return Page();
            }

            if (SelectedScreeningId == 0)
            {
                Message = ErrorMessages.ScreeningNotSelected;
                OnGet(); // כדי למלא את MoviesSelectList מחדש
                return Page();
            }

            return RedirectToPage("/SelectSeats", new { ScreeningId = SelectedScreeningId, NumTickets = NumTickets });

        }
    }
}