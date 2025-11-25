using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopAPI.DTOs
{
    public class OrderViewDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
        [Required, MaxLength(50)]
        public string Status { get; set; } = string.Empty;
        [Required]
        public IEnumerable<OrderItemViewDto> Items { get; set; }
    }

    public class OrderCreateDto
    {
        [Required]
        public int UserId { get; set; }
        [Required, MinLength(1)]
        public IEnumerable<OrderItemCreateDto> Items { get; set; }
        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }
        [Required, MaxLength(50)]
        public string Status { get; set; } = string.Empty;
    }

    public class OrderUpdateDto
    {
        [Required, MaxLength(50)]
        public string Status { get; set; } = string.Empty;
        [Required, MinLength(1)]
        public IEnumerable<OrderItemUpdateDto> Items { get; set; }
    }
}

