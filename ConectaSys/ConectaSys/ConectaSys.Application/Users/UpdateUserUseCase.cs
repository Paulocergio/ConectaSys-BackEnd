using ConectaSys.ConectaSys.Application.Users.ConectaSys.ConectaSys.Application.DTOs.Users;

namespace ConectaSys.ConectaSys.Application.Users.UseCases
{
    public class UpdateUserUseCase
    {
        private readonly UserService _userService;

        public UpdateUserUseCase(UserService userService)
        {
            _userService = userService;
        }

        public async Task<User> ExecuteAsync(Guid userId, UpdateUserRequest updateUserRequest)
        {
            if (userId != updateUserRequest.UserId)
            {
                throw new ArgumentException("O ID do usuário na URL não corresponde ao ID no corpo da requisição.");
            }

            return await _userService.UpdateUserAsync(updateUserRequest);
        }
    }
}
