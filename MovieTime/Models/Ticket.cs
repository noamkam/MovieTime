namespace MovieTime.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public int ScreeningId { get; set; }
        public Screening Screening { get; set; }

        public int SeatId { get; set; }
        public Seat Seat { get; set; }

        public int PurchaseId { get; set; } 
        public Purchase Purchase { get; set; }
    }
}
