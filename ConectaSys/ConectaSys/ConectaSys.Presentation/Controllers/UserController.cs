using ConectaSys.ConectaSys.Application.DTOs;
using ConectaSys.ConectaSys.Application.UserCases;
using Microsoft.AspNetCore.Mvc;
namespace ConectaSys.ConectaSys.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   
   
    public class UserController : ControllerBase
    {
        private readonly CreateUserCase _createUserCase;

        public UserController(CreateUserCase createUserCase)
        {
            _createUserCase = createUserCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO userDTO)
        {
            await _createUserCase.Execute(userDTO);
            return Created("", userDTO);
        }
    }
}

