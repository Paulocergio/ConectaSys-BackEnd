using ConectaSys.ConectaSys.Application.DTOs;
using ConectaSys.ConectaSys.Application.Users;
using ConectaSys.ConectaSys.Application.Users.ConectaSys.ConectaSys.Application.DTOs.Users;
using ConectaSys.ConectaSys.Application.Users.UseCases;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly GetAllUsersUseCase _getAllUsersUseCase;
    private readonly DeleteUserUseCase _deleteUserUseCase;
    private readonly LoginUserUseCase _loginUserUseCase;
    private readonly UpdateUserUseCase _updateUserUseCase;

    public UserController(
        CreateUserUseCase createUserUseCase,
        GetAllUsersUseCase getAllUsersUseCase,
        DeleteUserUseCase deleteUserUseCase,
        LoginUserUseCase loginUserUseCase,
        UpdateUserUseCase updateUserUseCase
        )
        
    {
        _createUserUseCase = createUserUseCase;
        _getAllUsersUseCase = getAllUsersUseCase;
        _deleteUserUseCase = deleteUserUseCase;
        _loginUserUseCase = loginUserUseCase;
        _updateUserUseCase = updateUserUseCase;
    }

  
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO userDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _createUserUseCase.ExecuteAsync(userDTO);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, new { message = "Usuário criado com sucesso." });
    }

   
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _getAllUsersUseCase.ExecuteAsync();
        return Ok(users);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var users = await _getAllUsersUseCase.ExecuteAsync();
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return NotFound(new { message = "Usuário não encontrado." });

        return Ok(user);
    }

   
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var deleted = await _deleteUserUseCase.ExecuteAsync(id);
        if (!deleted) return NotFound(new { message = "Usuário não encontrado." });

        return Ok(new { message = "Usuário deletado com sucesso." });
    }

    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            var token = await _loginUserUseCase.ExecuteAsync(loginDTO);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Credenciais inválidas." });
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserRequest updateUserRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updatedUser = await _updateUserUseCase.ExecuteAsync(id, updateUserRequest);
            return Ok(new { message = "Usuário atualizado com sucesso.", updatedUser });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno ao atualizar usuário.", error = ex.Message });
        }
    }
}
