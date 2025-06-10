using MovieTime.Messages;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Genre // מחלקת ז'אנר
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = AdminMessages.GenreNull)]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; } //הסרטים עם הז'אנר הזה
        //קשר יחיד לרבים - ז'אנר אחד יכול להיות בכמה סרטים
    }
}
