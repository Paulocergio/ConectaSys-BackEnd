using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConectaSys.ConectaSys.Application.DTOs.Products;
using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.ConectaSys.Domain.Interfaces.ProductRepository;

namespace ConectaSys.ConectaSys.Application.Products.Services 
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(CreateProductDTO productDTO)
        {
            var product = new Product
            {
                Id = productDTO.Id ?? Guid.NewGuid(), 
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
                  

        Cost = productDTO.Cost,
                ProfitMargin = productDTO.ProfitMargin,
                Discount = productDTO.Discount,
                UnitOfMeasure = productDTO.UnitOfMeasure,
                Tags = productDTO.Tags,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _productRepository.CreateProduct(product);
            return product;
        }


    }
}
