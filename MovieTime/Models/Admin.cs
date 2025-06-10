using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Admin //מחלקת מנהל
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; } 
    }
}
