using MySimpleNetApi.Models;

namespace MySimpleNetApi.Services;

public interface IProductService
{
    Task<List<Product>> List();
    Task<Product> RegisterProduct(Product product);
    Task<Product> UpdateProduct(Product newProduct);
    Task<Product> DeleteProduct(Product deleteProduct);
}