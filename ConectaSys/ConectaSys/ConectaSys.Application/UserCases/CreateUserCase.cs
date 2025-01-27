using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.ConectaSys.Application.DTOs.Users;
using ConectaSys.ConectaSys.Domain.Interfaces.Users;

namespace ConectaSys.ConectaSys.Application.UserCases
{
    public class CreateUserCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Execute(CreateUserDTO userDTO)
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



    }

}
