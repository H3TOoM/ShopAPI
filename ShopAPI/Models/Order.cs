using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
        [Required, MaxLength(50)]
        public string Status { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
