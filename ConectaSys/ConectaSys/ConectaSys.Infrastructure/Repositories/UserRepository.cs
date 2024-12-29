using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.ConectaSys.Domain.Interfaces;
using ConectaSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConectaSys.Infrastructure.Repositories
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
    }
}
