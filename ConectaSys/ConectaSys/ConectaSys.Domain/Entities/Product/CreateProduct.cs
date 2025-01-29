using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConectaSys.ConectaSys.Domain.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid(); 

        [Required]
        [Column("Name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column("Description", TypeName = "text")]
        public string Description { get; set; }

        [Required]
        [Column("Price", TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column("StockQuantity")]
        public int StockQuantity { get; set; }

        [Column("ProductCode", TypeName = "varchar(50)")]
        public string ProductCode { get; set; }

        [Column("Category", TypeName = "varchar(100)")]
        public string Category { get; set; }

        [Column("Brand", TypeName = "varchar(100)")]
        public string Brand { get; set; }

        [Column("Weight", TypeName = "decimal(10,2)")]
        public decimal Weight { get; set; }

        [Column("Dimensions", TypeName = "varchar(100)")]
        public string Dimensions { get; set; }

        [Column("ImageUrl", TypeName = "text")]
        public string ImageUrl { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("UpdatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("Status", TypeName = "varchar(50)")]
        public string Status { get; set; } = "active";

        [Column("SupplierId")]
        public Guid SupplierId { get; set; } = Guid.NewGuid();
        [Column("Cost", TypeName = "decimal(10,2)")]
        public decimal Cost { get; set; }

        [Column("ProfitMargin", TypeName = "decimal(5,2)")]
        public decimal ProfitMargin { get; set; }

        [Column("Discount", TypeName = "decimal(5,2)")]
        public decimal Discount { get; set; }

        [Column("UnitOfMeasure", TypeName = "varchar(50)")]
        public string UnitOfMeasure { get; set; }

        [Column("Tags", TypeName = "varchar(MAX)")]
        public string Tags { get; set; }
    }
}
