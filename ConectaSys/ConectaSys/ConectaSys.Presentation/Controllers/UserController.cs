using ConectaSys.ConectaSys.Application.DTOs;
using ConectaSys.ConectaSys.Application.UserCases;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly CreateUserCase _createUserCase;

    private readonly GetAllUsersCase _getAllUsersCase;

    public UserController(CreateUserCase createUserCase, GetAllUsersCase getAllUsersCase)
    {
        _createUserCase = createUserCase;
  
        _getAllUsersCase = getAllUsersCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO userDTO)
    {
        await _createUserCase.Execute(userDTO);
        return Created("", userDTO);
    }

   

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _getAllUsersCase.Execute();
        return Ok(users);
    }


}
