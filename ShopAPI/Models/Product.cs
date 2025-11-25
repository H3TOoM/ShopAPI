using System.ComponentModel.DataAnnotations;

namespace ShopAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Url, MaxLength(500)]
        public string ImageUrl { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }


    }
}
