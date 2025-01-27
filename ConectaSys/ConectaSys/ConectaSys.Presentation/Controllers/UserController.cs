using ConectaSys.ConectaSys.Application.DTOs.Users;
using ConectaSys.ConectaSys.Application.UserCases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly CreateUserCase _createUserCase;
    private readonly GetAllUsersCase _getAllUsersCase;
    private readonly LoginUserCase _loginUserCase;
    private readonly DeleteUserCase _deleteUserCase;
    private readonly ILogger<UserController> _logger;

    public UserController( DeleteUserCase deleteUserCase, CreateUserCase createUserCase, GetAllUsersCase getAllUsersCase, LoginUserCase loginUserCase, ILogger<UserController> logger)
    {
        _createUserCase = createUserCase;
        _getAllUsersCase = getAllUsersCase;
        _loginUserCase = loginUserCase;
        _logger = logger;
        _deleteUserCase = deleteUserCase;
    } 
    [HttpPost("CrateUser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO userDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {

            var user = await _createUserCase.Execute(userDTO);

            _logger.LogInformation("Usuário criado com sucesso: {Email}", userDTO.Email);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, new { message = "Usuário criado com sucesso." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar usuário: {Email}", userDTO.Email);
            return StatusCode(500, new { message = "Erro interno no servidor." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var users = await _getAllUsersCase.Execute();
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null)
            return NotFound(new { message = "Usuário não encontrado." });

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _deleteUserCase.DeleteUser(id);
        if (user == null)
            return NotFound(new { message = "Usuário não encontrado." });

        return Ok(new { message = "Usuário deletado com sucesso." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var requestId = Guid.NewGuid();
        _logger.LogInformation("Login request iniciado. RequestId: {RequestId}, Email: {Email}", requestId, loginDTO.Email);

        try
        {
            var token = await _loginUserCase.Execute(loginDTO);
            _logger.LogInformation("Login realizado com sucesso. RequestId: {RequestId}, Email: {Email}", requestId, loginDTO.Email);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Login falhou. RequestId: {RequestId}, Email: {Email}. Motivo: {Message}", requestId, loginDTO.Email, ex.Message);
            return Unauthorized(new { Message = ex.Message });
        }
    }



    [HttpGet("log")]
    public IActionResult TestLog()
    {
        _logger.LogInformation("Teste de log: usuário acessou o endpoint.");
        return Ok("Log registrado.");
    }
}
