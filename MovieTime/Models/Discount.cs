using System.ComponentModel.DataAnnotations;

namespace MovieTime.Models
{
    public class Discount // מחלקת הנחה
    {
        [Required]
        public int DiscountId { get; set; }
        public string Code { get; set; }  
        public string Description { get; set; }
        public double Percentage { get; set; }  
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Purchase> Purchases { get; set; }//רכישות שמשתמשות בהנחה 
        // קשר יחיד לרבים - קופון אחד יכול להיות בכמה רכישות
    }
}
