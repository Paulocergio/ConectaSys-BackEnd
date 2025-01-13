using ConectaSys.ConectaSys.Application.DTOs.Product;
using ConectaSys.ConectaSys.Application.UserCases;
using Microsoft.AspNetCore.Mvc;

namespace ConectaSys.ConectaSys.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly CreateProductCase _createProductCase;

        public ProductController(CreateProductCase createProductCase)
        {
            _createProductCase = createProductCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _createProductCase.Execute(productDTO);
            return Ok(new { message = "Produto criado com sucesso." });
        }
    }
}
