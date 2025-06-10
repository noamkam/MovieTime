using MovieTime.Messages;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Hall // מחלקת אולם
    {
        public int HallId { get; set; }

        [Required(ErrorMessage = SeatMessages.SeatCountlRequired)]
        public int SeatCount { get; set; }
        public bool IsVIP { get; set; } = false;
        public ICollection<Screening> Screenings { get; set; } //הקרנות שמתרחשות באולם זה
        //קשר יחיד לרבים - באולם אחד יכולות להיות כמה הקרנות
        public ICollection<Seat> Seats { get; set; } // רשימת כיסאות באולם זה
        //קשר יחיד לרבים - באולם אחד יכולים להיות כמה כיסאות

    }
}
