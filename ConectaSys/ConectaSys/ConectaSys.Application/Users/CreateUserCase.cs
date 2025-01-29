public class CreateUserUseCase
{
    private readonly UserService _userService;

    public CreateUserUseCase(UserService userService)
    {
        _userService = userService;
    }

    public async Task<User> ExecuteAsync(CreateUserDTO userDTO)
    {
        return await _userService.CreateUserAsync(userDTO);
    }
}
