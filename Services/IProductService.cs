using MySimpleNetApi.Models;

namespace MySimpleNetApi.Services;

public interface IProductService
{
    Task<List<Product>> List();
    Task<Product> RegisterProduct(Product product);
    Task<Product> UpdateProduct(string id, Product product);
    Task<Product> DeleteProduct(string id);
}