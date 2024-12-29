using ConectaSys.ConectaSys.Domain.Interfaces;
using ConectaSys.ConectaSys.Domain.Entities;

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
