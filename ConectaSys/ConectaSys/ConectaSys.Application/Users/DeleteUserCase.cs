public class DeleteUserUseCase
{
    private readonly UserService _userService;

    public DeleteUserUseCase(UserService userService)
    {
        _userService = userService;
    }

    public async Task<bool> ExecuteAsync(Guid userId)
    {
        return await _userService.DeleteUserAsync(userId);
    }
}
