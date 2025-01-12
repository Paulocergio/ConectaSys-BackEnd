using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaSys.ConectaSys.Domain.Entities
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public decimal Weight { get; set; }
        public string Dimensions { get; set; }
        public string ImageUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "active";
        public decimal Cost { get; set; }
        public decimal ProfitMargin { get; set; }
        public decimal Discount { get; set; }
    }
}
