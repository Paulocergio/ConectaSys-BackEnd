using ConectaSys.ConectaSys.Domain.Entities;
using ConectaSys.ConectaSys.Domain.Interfaces.ProductRepository;
using ConectaSys.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ConectaSys.ConectaSys.Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository : ICreateProduct
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateProduct(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
