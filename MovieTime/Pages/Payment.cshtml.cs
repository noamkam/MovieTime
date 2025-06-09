using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MovieTime.Context;
using MovieTime.Messages;
using MovieTime.Models;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Pages
{
    public class PaymentModel : PageModel
    {
        public MovieTimeDBContext _context;

        [BindProperty]
        public string AccountNumber { get; set; }

        [BindProperty]

        public string CreditCardNumer { get; set; }
        [BindProperty]
        public string OwnerId { get; set; }

        [BindProperty]
        public string CVV { get; set; }
        [BindProperty]
        public string ValidityMonth { get; set; }
        [BindProperty]
        public string ValidityYear { get; set; }
        [BindProperty(SupportsGet = true)]

        public List<int> SelectedSeats { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ScreeningId { get; set; }
        [BindProperty(SupportsGet = true)]
        public string CustomerId { get; set; }

        [BindProperty]
        public string Code { get; set; }

        public string ErrorMessage { get; set; }
        public IConfiguration Configuration { get; set; }

        public PaymentModel(MovieTimeDBContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //get the owner account details
            var account = _context.BankService.Where(a => a.AccountNumber == AccountNumber).FirstOrDefault();
            if (account == null || account.OwnerId != OwnerId || account.CreditCardNumber != CreditCardNumer || account.CVV != CVV || account.ValidityMonth != ValidityMonth || account.ValidityYear != ValidityYear)
            {
                ErrorMessage = PaymentMessages.DetailsInvalid;
                return Page();
            }

            if (account.Balance < SelectedSeats.Count * Configuration.GetValue<int>("TicketPrice"))
            {
              //  ErrorMessage = "Insufficient balance.";
                return Page();
            }
            // ErrorMessage = LoginMessages.InvalidCredentials;

            float discountPercentage = 0;

            if (Code != null)
            {
                var discount = _context.Discounts.Where(d => d.Code == Code).FirstOrDefault();
                if(discount == null)
                {
                    ErrorMessage = PaymentMessages.CodeInvalid;
                    return Page();
                }
                else
                {
                    discountPercentage = (float)discount.Percentage;
                }
            }


            var newPurchase = new Purchase
            {
                PurchaseDate = DateTime.Now,
                CustomerId = int.Parse(CustomerId),
                TotalPrice = (decimal)SelectedSeats.Count * Configuration.GetValue<int>("TicketPrice") * (100 - (decimal)discountPercentage) / 100,
                DiscountId = Code != null ? _context.Discounts.Where(d => d.Code == Code).Select(d => d.DiscountId).FirstOrDefault() : (int?)null
            };
            _context.Purchases.Add(newPurchase);
            await _context.SaveChangesAsync(); 


            foreach (var seat in SelectedSeats)
            {
                _context.Tickets.Add(new Ticket
                {
                    ScreeningId = ScreeningId,
                    SeatId = seat,
                    PurchaseId = newPurchase.PurchaseId
                });
            }
            await _context.SaveChangesAsync();
            var movieId = _context.Screenings.Where(s => s.ScreeningId == ScreeningId).Select(s => s.MovieId).FirstOrDefault();
            var movieTitle = _context.Movies.Where(m => m.Id == movieId).Select(m => m.Title).FirstOrDefault();
            var hallId = _context.Screenings.Where(s => s.ScreeningId == ScreeningId).Select(s => s.HallId).FirstOrDefault();
            var seats = _context.Tickets.Where(t => t.ScreeningId == ScreeningId && t.PurchaseId == newPurchase.PurchaseId).Select(t => t.SeatId).ToList();
            var screeningDateTime = _context.Screenings.Where(s => s.ScreeningId == ScreeningId).Select(s => s.ScreeningDateTime).FirstOrDefault();
            await _context.SaveChangesAsync();
            return  RedirectToPage("PurchaseConfirmation", new { MovieTitle = movieTitle, HallNumber = hallId, SeatNumbers = seats, ScreeningDateTime = screeningDateTime, PurchaseId = newPurchase.PurchaseId });
        }
    }
}
