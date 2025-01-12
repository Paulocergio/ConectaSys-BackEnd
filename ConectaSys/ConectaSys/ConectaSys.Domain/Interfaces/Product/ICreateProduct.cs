using ConectaSys.ConectaSys.Domain.Entities;


namespace ConectaSys.ConectaSys.Domain.Interfaces.ProductRepository
{
    public interface ICreateProduct
    {
        Task CreateProduct(Product product);


    }
}
