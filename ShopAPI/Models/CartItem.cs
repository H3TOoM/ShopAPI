using System.ComponentModel.DataAnnotations;

namespace ShopAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }

        public Cart Cart { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

    }
}
