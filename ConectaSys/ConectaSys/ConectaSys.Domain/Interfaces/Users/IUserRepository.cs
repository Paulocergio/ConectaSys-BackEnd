using ConectaSys.ConectaSys.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConectaSys.ConectaSys.Domain.Interfaces.Users
{
    public interface IUserRepository
    {
      
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<User> ValidateUserAsync(string email, string password);
    }
}
