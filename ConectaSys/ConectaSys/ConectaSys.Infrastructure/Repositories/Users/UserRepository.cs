using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.ConectaSys.Domain.Interfaces.Users;
using ConectaSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConectaSys.ConectaSys.Infrastructure.Repositories.Users
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
            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar usuário ao banco de dados.", ex);
            }
        }

        public async Task<User> DeleteUser(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return null;

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
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