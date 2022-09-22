using MySimpleNetApi.Models;

namespace MySimpleNetApi.Repository;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<Product?> FindById(string id);
    Task Save(Product product);
    void Update(Product product);
    void Delete(Product product);
}