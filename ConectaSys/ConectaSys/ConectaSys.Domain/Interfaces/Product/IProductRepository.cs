using ConectaSys.ConectaSys.Domain.Entities;


namespace ConectaSys.ConectaSys.Domain.Interfaces.ProductRepository
{
    public interface IProductRepository
    {
        Task CreateProduct(Product product);


    }
}
