using ConectaSys.ConectaSys.Application.UserCases;
using ConectaSys.ConectaSys.Domain.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConectaSys.ConectaSys.Application.UserCases
{
    public class DeleteUserCase
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> DeleteUser(Guid id)
        {
            var user = await _userRepository.DeleteUser(id);
            if (user == null)
                return null;

            await _userRepository.DeleteUser(id);
            return user;
        }
    }
}
