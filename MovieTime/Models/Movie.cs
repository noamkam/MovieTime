using MovieTime.Messages;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = MovieMessages.MovieRequierd)]
        public string Title { get; set; }

        [Required(ErrorMessage = MovieMessages.DescriptionRequierd)]
        public string Description { get; set; }

        [Required(ErrorMessage = MovieMessages.GenreRequierd)]
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }

        [Required(ErrorMessage = MovieMessages.ReleaseDateRequierd)]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = MovieMessages.ImageRequierd)]
        public string Image { get; set; }

        [Required(ErrorMessage = MovieMessages.DurationRequierd)]
        public int Duration { get; set; }

        [Required(ErrorMessage = MovieMessages.LanguageRequierd)]
        public int LanguageId { get; set; }
        public Language? Language { get; set; } 
        public bool DubbedIntoHebrew { get; set; }
        public ICollection<Screening> Screenings { get; set; } // One to Many relationship with Screening
    }
}
