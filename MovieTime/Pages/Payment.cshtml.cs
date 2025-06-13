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
        [Required(ErrorMessage = PaymentMessages.AccountNumberNull)]
        public string AccountNumber { get; set; }

        [BindProperty]
        [Required(ErrorMessage = PaymentMessages.CreditCardNumberNull)]
        public string CreditCardNumer { get; set; }

        [BindProperty]
        [Required(ErrorMessage = PaymentMessages.OwnerIdNull)]
        public string OwnerId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = PaymentMessages.CVVNull)]
        public string CVV { get; set; }

        [BindProperty]
        [Required(ErrorMessage = PaymentMessages.ValidityMonthNull)]
        public string ValidityMonth { get; set; }

        [BindProperty]
        [Required(ErrorMessage = PaymentMessages.ValidityYearNull)]
        public string ValidityYear { get; set; }

        [BindProperty(SupportsGet = true)]

        public List<int> SelectedSeats { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ScreeningId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CustomerId { get; set; }

        [BindProperty]
        public string? Code { get; set; }
        public string ErrorMessage { get; set; }
        public IConfiguration Configuration { get; set; } //ניגש לערכים מהאפסטינגס

        public PaymentModel(MovieTimeDBContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public async Task<IActionResult> OnPost()
        {
            // בדיקה אם קיים חשבון בנק כמו שהוזן
            var account = _context.BankService.Where(a => a.AccountNumber == AccountNumber).FirstOrDefault();
            if (account == null || account.OwnerId != OwnerId || account.CreditCardNumber != CreditCardNumer || account.CVV != CVV || account.ValidityMonth != ValidityMonth || account.ValidityYear != ValidityYear)
            {
                ErrorMessage = PaymentMessages.DetailsInvalid;
                return Page();
            }
            // אם אין מספיק יתרה בחשבון בשביל רכישת הכרטיסים
            if (account.Balance < SelectedSeats.Count * Configuration.GetValue<int>("TicketPrice"))
            {
               ErrorMessage = PaymentMessages.NotEnoughBalance;
                return Page();
            }
            float discountPercentage = 0;

            // אם הוזן קוד קופון אבדוק אם קיים במסד
            if (!string.IsNullOrEmpty(Code))
            {
                var discount = _context.Discounts.Where(d => d.Code == Code).FirstOrDefault();
                if (discount == null)
                { 
                    ErrorMessage = PaymentMessages.CodeInvalid;
                    return Page();
                }
                else
                {
                    //חישוב אחוזי ההנחה
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
