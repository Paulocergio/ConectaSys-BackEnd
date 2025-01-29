using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.ConectaSys.Domain.Interfaces.Users;
using ConectaSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConectaSys.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _dbContext.Users.FindAsync(user.Id);
            if (existingUser == null)
                throw new Exception("Usuário não encontrado.");


            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            existingUser.Phone = user.Phone;

            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                existingUser.PasswordHash = user.PasswordHash;
            }

            _dbContext.Users.Update(existingUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
                return false;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> ValidateUserAsync(string email, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null;

            var isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return isValidPassword ? user : null;
        }
    }
}
