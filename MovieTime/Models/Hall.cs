using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Hall
    {
        [Required]
        public int HallId { get; set; }
        public int SeatCount { get; set; }
        public bool IsVIP { get; set; } = false;
        public ICollection<Screening> Screenings { get; set; } // One to Many relationship with Screening
        public ICollection<Seat> Seats { get; set; } // One to Many relationship with Seat

    }
}
