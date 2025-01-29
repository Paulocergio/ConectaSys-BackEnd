using System.ComponentModel.DataAnnotations;

namespace ConectaSys.ConectaSys.Application.DTOs.Products
{
    public class CreateProductDTO
    {
        public Guid? Id { get; set; } // ✅ Agora `Id` é opcional e um GUID
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
        public Guid? SupplierId { get; set; } // ⚠️ Verifique se SupplierId também precisa ser `Guid`
        public decimal Cost { get; set; }
        public decimal ProfitMargin { get; set; }
        public decimal Discount { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Tags { get; set; }
    }



}
