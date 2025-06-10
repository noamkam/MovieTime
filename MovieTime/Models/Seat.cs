using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Seat // מחלקת כיסא
    {
        [Required]
        public int SeatId { get; set; }
        public int HallId { get; set; } 
        public Hall Hall { get; set; } 
        public int SeatNumber { get; set; }
    }
}
