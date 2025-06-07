using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Seat
    {
        [Required]
        public int SeatId { get; set; }
        public int HallId { get; set; } //FK for Hall
        public Hall Hall { get; set; } 
        public int SeatNumber { get; set; }
    }
}
