namespace ConectaSys.ConectaSys.Application.DTOs.Product
{
    public class CreateProductDTO
    {
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


    }
}
