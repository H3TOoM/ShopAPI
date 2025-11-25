using System.ComponentModel.DataAnnotations;

namespace ShopAPI.DTOs
{
    public class ProductViewDto
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Url, MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }
    }

    public class ProductCreateDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Url, MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }
    }

    public class ProductUpdateDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        [Url, MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public int CategoryId { get; set; }
    }
}

