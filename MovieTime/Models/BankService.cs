using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class BankService //מחלקת בנק
    {
        public string OwnerName { get; set; }
        public string OwnerId { get; set; }
        [Key]
        [Required]
        public string AccountNumber { get; set; }
        public string CreditCardNumber { get; set; }
        public string CVV { get; set; }
        public string ValidityMonth { get; set; }
        public string ValidityYear { get; set; }
        public int Balance { get; set; }
    }
}
