using MovieTime.Messages;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Genre
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = AdminMessages.GenreNull)]
        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
