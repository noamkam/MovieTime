using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace MovieTime.Models
{
    public class Purchase // מחלקת רכישה
    {
        [Required]
        public int PurchaseId { get; set; }

        [Required]
        public int CustomerId { get; set; } 

        public Customer Customer { get; set; } 

        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        public int? DiscountId { get; set; } 

        public Discount Discount { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<Ticket> Tickets { get; set; } // הכרטיסים שנמכרו ברכישה 
        //קשר יחיד לרבים - כל רכישה יכול להכיל כמה כרטיסים

    }
}
