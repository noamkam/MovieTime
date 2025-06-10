using MovieTime.Messages;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Language // מחלקת שפה
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage =LanguageMessages.LanguageNull)]

        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; } // כל הסרטים בשפה הזו
        // קשר יחיד לרבים - כל שפה יכולה להיות בכמה סרטים
    }
}
