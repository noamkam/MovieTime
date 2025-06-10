using MovieTime.Messages;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Screening
    {
        public int ScreeningId { get; set; }
        [Required(ErrorMessage = ScreeningMessages.MovieRequierd)]
        public int MovieId { get; set; }  
        public Movie Movie { get; set; }

        [Required(ErrorMessage = ScreeningMessages.HallRequierd)]

        public int HallId { get; set; } 
        public Hall Hall { get; set; }

        [Required(ErrorMessage = ScreeningMessages.ScreeningRequierd)]
        public DateTime ScreeningDateTime { get; set; }
        public ICollection<Ticket> Tickets { get; set; } 

    }
}
