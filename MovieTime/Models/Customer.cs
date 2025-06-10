using MovieTime.Messages;
using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }
        
        [Required(ErrorMessage = RegistrationMessages.NameNull)]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress(ErrorMessage = ValidationsMessages.EmailInvalid)]
        public string Email { get; set; }
        
        [Required]
        [Phone(ErrorMessage = ValidationsMessages.PhoneInvalid)]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; } 

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public ICollection<Purchase> Purchases { get; set; } 
    }
}
