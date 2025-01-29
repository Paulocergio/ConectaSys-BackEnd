using ConectaSys.ConectaSys.Application.DTOs;
using ConectaSys.ConectaSys.Application.Users.ConectaSys.ConectaSys.Application.DTOs.Users;
using ConectaSys.ConectaSys.Domain.Interfaces.Users;


public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(CreateUserDTO userDTO)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
        var user = new User
        {
            Name = userDTO.Name,
            Email = userDTO.Email,
            PasswordHash = passwordHash,
            Role = userDTO.Role,
            Phone = userDTO.Phone,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _userRepository.AddAsync(user);
        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User> UpdateUserAsync(UpdateUserRequest updateUserRequest)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(updateUserRequest.UserId);
        if (existingUser == null)
            throw new Exception("Usuário não encontrado.");

        existingUser.Name = updateUserRequest.Name;
        existingUser.Email = updateUserRequest.Email;
        existingUser.Role = updateUserRequest.Role;
        existingUser.Phone = updateUserRequest.Phone;

        if (!string.IsNullOrEmpty(updateUserRequest.Password))
        {
            existingUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserRequest.Password);
        }

        await _userRepository.UpdateUserAsync(existingUser);
        return existingUser;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }

    public async Task<string> LoginAsync(LoginDTO loginDTO)
    {
        var user = await _userRepository.ValidateUserAsync(loginDTO.Email, loginDTO.Password);
        if (user == null)
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        return "token_aqui";
    }
}
