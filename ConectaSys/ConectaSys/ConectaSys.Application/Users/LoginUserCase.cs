using ConectaSys.ConectaSys.Application.DTOs;
using ConectaSys.ConectaSys.Domain.Interfaces.Users;
using ConectaSys.ConectaSys.Infrastructure.Security;
using System;
using System.Threading.Tasks;

namespace ConectaSys.ConectaSys.Application.Users.UseCases
{
    public class LoginUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public LoginUserUseCase(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> ExecuteAsync(LoginDTO loginDTO)
        {
            var user = await _userRepository.ValidateUserAsync(loginDTO.Email, loginDTO.Password);
            if (user == null)
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            return _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.Role);
        }
    }
}
