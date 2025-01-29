using System.Threading.Tasks;
using ConectaSys.ConectaSys.Application.DTOs.Products;
using ConectaSys.ConectaSys.Application.Products.Services;
using ConectaSys.ConectaSys.Domain.Entities;

namespace ConectaSys.ConectaSys.Application.Products.UseCases
{
    public class CreateProductUseCase
    {
        private readonly ProductService _productService; 

        public CreateProductUseCase(ProductService productService) 
        {
            _productService = productService;
        }

        public async Task<Product> ExecuteAsync(CreateProductDTO productDTO)
        {
            return await _productService.CreateProductAsync(productDTO);
        }
    }
}
