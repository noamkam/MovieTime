using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace MovieTime.Models
{
    public class Purchase
    {
        [Required]
        public int PurchaseId { get; set; }

        [Required]
        public int CustomerId { get; set; } // FK to Customer

        public Customer Customer { get; set; } 

        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        public int? DiscountId { get; set; } // Nullable if no discount used

        public Discount Discount { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<Ticket> Tickets { get; set; } // All tickets in this purchase

    }
}
