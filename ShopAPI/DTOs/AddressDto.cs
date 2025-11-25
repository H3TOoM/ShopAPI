using System.ComponentModel.DataAnnotations;

namespace ShopAPI.DTOs
{
    public class AddressViewDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required, MaxLength(200)]
        public string Street { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string State { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Country { get; set; } = string.Empty;
    }

    public class AddressCreateDto
    {
        [Required]
        public int UserId { get; set; }
        [Required, MaxLength(200)]
        public string Street { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string State { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Country { get; set; } = string.Empty;
    }

    public class AddressUpdateDto
    {
        [Required, MaxLength(200)]
        public string Street { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string State { get; set; } = string.Empty;
        [Required, MaxLength(20)]
        public string PostalCode { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string Country { get; set; } = string.Empty;
    }
}

