using ConectaSys.ConectaSys.Domain.Interfaces;
using ConectaSys.ConectaSys.Application.DTOs;
using ConectaSys.ConectaSys.Domain.Entities;

namespace ConectaSys.ConectaSys.Application.UserCases
{
    public class LoginUserCase
    {
        private readonly IUserRepository _userRepository;

        public LoginUserCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Execute(LoginDTO loginDTO)
        {
            var user = await _userRepository.ValidateUserAsync(loginDTO.Email, loginDTO.Password);

            if (user == null)
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            return user;
        }
    }
}
