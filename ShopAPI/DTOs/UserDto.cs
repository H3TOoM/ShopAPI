using System.ComponentModel.DataAnnotations;

namespace ShopAPI.DTOs
{
    public class UserViewDto
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Role { get; set; } = string.Empty;
    }

    public class UserCreateDto
    {
        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Role { get; set; } = string.Empty;
    }

    public class UserLoginDto
    {
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }

    public class UserUpdateDto
    {
        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required, EmailAddress, MaxLength(150)]
        public string Email { get; set; } = string.Empty;
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Role { get; set; } = string.Empty;
    }
}

