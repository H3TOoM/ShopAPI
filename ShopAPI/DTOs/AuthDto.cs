using System.ComponentModel.DataAnnotations;

namespace ShopAPI.DTOs
{
    public class AuthResponseDto
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        public DateTime ExpiresAt { get; set; }

        [Required]
        public UserViewDto User { get; set; } = default!;
    }
}



