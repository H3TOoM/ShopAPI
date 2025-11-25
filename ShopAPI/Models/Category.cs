using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopAPI.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
