using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTime.Context;
using System;
using System.Collections.Generic;

namespace MovieTime.Pages
{
    public class PurchaseConfirmationModel : PageModel
    {
        private readonly MovieTimeDBContext _context;

        public PurchaseConfirmationModel(MovieTimeDBContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string MovieTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ScreeningDateTime { get; set; }

        [BindProperty(SupportsGet = true)]
        public string HallNumber { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<int> SeatNumbers { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PurchaseId { get; set; }

        public string FormattedDateTime { get; set; }

        public IActionResult OnGet()
        {
            if (SeatNumbers == null)
            {
                SeatNumbers = new List<int>();
            }

            // פורמט נוח להצגה אם נדרש
            if (DateTime.TryParse(ScreeningDateTime, out var parsedDate))
            {
                FormattedDateTime = parsedDate.ToString("dd/MM/yyyy HH:mm");
            }
            else
            {
                FormattedDateTime = ScreeningDateTime; // במידה ולא פורמט תקין
            }

            return Page();
        }
    }
}
