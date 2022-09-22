using MySimpleNetApi.Exceptions;
using MySimpleNetApi.Models;

namespace MySimpleNetApi.Services;

public class ProductService : IProductService
{
    private List<Product> listProduct = new()
    {
        new Product
        {
            Id = "1",
            ProductName = "Juice Melon"
        },
        new Product
        {
            Id = "2",
            ProductName = "Pecel Ayam"
        },
    };

    public Task<List<Product>> List()
    {
        return Task.Run(() => listProduct);
    }

    public Task<Product> RegisterProduct(Product product)
    {
        return Task.Run(() =>
        {
            listProduct.Add(product);
            return product;
        });
    }

    public Task<Product> UpdateProduct(string id, Product product)
    {
        return Task.Run(() =>
        {
            var result = listProduct.FirstOrDefault(p => p.Id == id);
            if (result == null) throw new NotFoundException("Product Not Found");
            result.ProductName = product.ProductName;
            return result;

        });
    }

    public Task<Product> DeleteProduct(string id)
    {
        return Task.Run(() =>
        {
            var result = listProduct.SingleOrDefault(p => p.Id == id);
            if (result == null) throw new NotFoundException("Product Not Found");
            listProduct.Remove(result);
            return result;
        });
    }
}