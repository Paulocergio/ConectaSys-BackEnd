public class GetAllUsersUseCase
{
    private readonly UserService _userService;

    public GetAllUsersUseCase(UserService userService)
    {
        _userService = userService;
    }

    public async Task<IEnumerable<User>> ExecuteAsync()
    {
        return await _userService.GetAllUsersAsync();
    }
}
