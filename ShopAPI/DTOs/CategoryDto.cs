using System.ComponentModel.DataAnnotations;

namespace ShopAPI.DTOs
{
    public class CategoryViewDto
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryCreateDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
    }

    public class CategoryUpdateDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
    }
}

