using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopAPI.DTOs
{
    public class CartViewDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public IEnumerable<CartItemViewDto> Items { get; set; }
    }

    public class CartCreateDto
    {
        [Required]
        public int UserId { get; set; }
        [Required, MinLength(1)]
        public IEnumerable<CartItemCreateDto> Items { get; set; }
    }

    public class CartUpdateDto
    {
        [Required, MinLength(1)]
        public IEnumerable<CartItemUpdateDto> Items { get; set; }
    }
}


