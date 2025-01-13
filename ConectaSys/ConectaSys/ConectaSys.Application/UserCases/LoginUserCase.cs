using ConectaSys.ConectaSys.Application.DTOs.Users;
using ConectaSys.ConectaSys.Application.Services;
using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.ConectaSys.Domain.Interfaces.Users;
using System;
using System.Threading.Tasks;

namespace ConectaSys.ConectaSys.Application.UserCases
{
    public class LoginUserCase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public LoginUserCase(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Execute(LoginDTO loginDTO)
        {
            var user = await _userRepository.ValidateUserAsync(loginDTO.Email, loginDTO.Password);
            if (user == null)
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.Role);

            return token;
        }
    }
}
