﻿using ConectaSys.ConectaSys.Domain.Entities;

namespace ConectaSys.ConectaSys.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> ValidateUserAsync(string email, string password);
    }


}
