using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } 
        public string? Description { get; set; }
        public int GenreId { get; set; } //FK for Genere
        public Genre? Genre { get; set; } 
        public DateTime? ReleaseDate { get; set; }
        public string? Image { get; set; } 
        public int? Duration { get; set; } // Duration in minutes
        public int LanguageId { get; set; } //FK for Language
        public Language? Language { get; set; } 
        public bool? DubbedIntoHebrew { get; set; }
        public ICollection<Screening> Screenings { get; set; } // One to Many relationship with Screening
    }
}
