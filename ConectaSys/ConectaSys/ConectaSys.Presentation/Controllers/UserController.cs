using ConectaSys.ConectaSys.Application.DTOs;
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
    private readonly ILogger<UserController> _logger;

    public UserController(CreateUserCase createUserCase, GetAllUsersCase getAllUsersCase, LoginUserCase loginUserCase, ILogger<UserController> logger)
    {
        _createUserCase = createUserCase;
        _getAllUsersCase = getAllUsersCase;
        _loginUserCase = loginUserCase;
        _logger = logger; 
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO userDTO)
    {
        await _createUserCase.Execute(userDTO);
        _logger.LogInformation("Usuário criado com sucesso: {Email}", userDTO.Email); 
        return Created("", userDTO);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _getAllUsersCase.Execute();
        _logger.LogInformation("Listagem de usuários acessada.");
        return Ok(users);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Tentativa de login com dados inválidos.");
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _loginUserCase.Execute(loginDTO);
            _logger.LogInformation("Login realizado com sucesso para o usuário: {Email}", loginDTO.Email);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Tentativa de login falhou: {Email}. Motivo: {Message}", loginDTO.Email, ex.Message);
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
