using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieTime.Context;
using MovieTime.Models;
using System;

namespace MovieTime.Pages
{
    public class SelectSeatsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ScreeningId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int NumTickets { get; set; }

        [BindProperty]
        public List<int> SelectedSeats { get; set; }

        public List<int> HallSeats { get; set; } = new List<int>();
        public List<int> TakenSeats { get; set; } = new List<int>();

        private readonly MovieTimeDBContext _context;
        public IConfiguration Configuration { get; set; }
        public string Message { get; set; }
        public SelectSeatsModel(MovieTimeDBContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(int screeningId, int tickets)
        {
            ScreeningId = screeningId;
            NumTickets = tickets;
            var hall = await _context.Screenings
                .Where(s => s.ScreeningId == ScreeningId)
                .Select(s => s.Hall)
                .FirstOrDefaultAsync();
           
            HallSeats = Enumerable.Range(1, hall.SeatCount).ToList();

            TakenSeats = await _context.Tickets
                .Where(t => t.ScreeningId == ScreeningId)
                .Select(t => t.SeatId)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedSeats == null || SelectedSeats.Count != NumTickets)
            {
                ModelState.AddModelError("", $"יש לבחור בדיוק {NumTickets} מושבים.");
                return await OnGetAsync(ScreeningId, NumTickets);
            }

            var customerId = HttpContext.Session.GetString("CustomerId");

            return RedirectToPage("/Payment", new { SelectedSeats = SelectedSeats, CustomerId = customerId, ScreeningId = ScreeningId});

        }
    }
}