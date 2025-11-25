using System.ComponentModel.DataAnnotations;

namespace ShopAPI.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required, MaxLength(200)]
        public string Street { get; set; }
        [Required, MaxLength(100)]
        public string City { get; set; }
        [Required, MaxLength(100)]
        public string State { get; set; }
        [Required, MaxLength(20)]
        public string PostalCode { get; set; }
        [Required, MaxLength(100)]
        public string Country { get; set; }
    }
}
