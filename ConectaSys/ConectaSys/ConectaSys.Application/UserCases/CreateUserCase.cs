using ConectaSys.ConectaSys.Domain.Interfaces;
using ConectaSys.ConectaSys.Application.DTOs;
using ConectaSys.ConectaSys.Domain.Entities;

namespace ConectaSys.ConectaSys.Application.UserCases
{
    public class CreateUserCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Execute(CreateUserDTO userDTO)
        {
      
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);          
            var user = new User(userDTO.Name, userDTO.Email, passwordHash , userDTO.Role , userDTO.Phone) ;
            await _userRepository.AddAsync(user);
        }
    }
}
