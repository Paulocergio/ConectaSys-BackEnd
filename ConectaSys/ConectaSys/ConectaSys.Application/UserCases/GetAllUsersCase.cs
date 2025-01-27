using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.ConectaSys.Domain.Interfaces.Users;

namespace ConectaSys.ConectaSys.Application.UserCases
{
    public class GetAllUsersCase
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> Execute()
        {
            return await _userRepository.GetAllAsync();
        }

      
    }
}
