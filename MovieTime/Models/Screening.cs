namespace MovieTime.Models
{
    public class Screening
    {
        public int ScreeningId { get; set; }

        public int MovieId { get; set; }  // FK for Movie
        public Movie Movie { get; set; }  

        public int HallId { get; set; } // FK for Hall
        public Hall Hall { get; set; }

        public DateTime ScreeningDateTime { get; set; }
        public ICollection<Ticket> Tickets { get; set; } // One to Many relationship with Tickets

    }
}
