using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }


    }
}
