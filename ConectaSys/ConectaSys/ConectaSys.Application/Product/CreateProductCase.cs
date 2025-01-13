using ConectaSys.ConectaSys.Application.DTOs.Product;
using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.ConectaSys.Domain.Interfaces.ProductRepository;

namespace ConectaSys.ConectaSys.Application.UserCases
{
    public class CreateProductCase
    {
        private readonly ICreateProduct _productRepository;

        public CreateProductCase(ICreateProduct productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Execute(CreateProductDTO productDTO)
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                StockQuantity = productDTO.StockQuantity,
                ProductCode = productDTO.ProductCode,
                Category = productDTO.Category,
                Brand = productDTO.Brand,
                Weight = productDTO.Weight,
                Dimensions = productDTO.Dimensions,
                ImageUrl = productDTO.ImageUrl,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = "active"
            };

            await _productRepository.CreateProduct(product);
        }
    }
}
