using System;
using System.Threading.Tasks;
using ConectaSys.ConectaSys.Application.DTOs.Products;
using ConectaSys.ConectaSys.Application.Products.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace ConectaSys.ConectaSys.Presentation.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly CreateProductUseCase _createProductUseCase;

        public ProductController(CreateProductUseCase createProductUseCase)
        {
            _createProductUseCase = createProductUseCase;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _createProductUseCase.ExecuteAsync(productDTO);

            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id });
        }
    }
}
