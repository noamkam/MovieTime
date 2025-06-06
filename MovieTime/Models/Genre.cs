using MovieTime.Messages;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Genre
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = ValidationsMessages.NameLengthLongerThan100Ch)]
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }//One to Many relationship with Movie
    }
}
