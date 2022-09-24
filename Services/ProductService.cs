using MySimpleNetApi.Exceptions;
using MySimpleNetApi.Models;
using MySimpleNetApi.Repository;

namespace MySimpleNetApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPersistence _persistence;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository,
        IPersistence persistence)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _persistence = persistence;
    }

    public Task<List<Product>> List()
    {
        return _productRepository.GetAll();
    }

    public async Task<Product> RegisterProduct(Product product)
    {
        await _productRepository.Save(product);
        await _persistence.Complete();
        return product;
    }

    public async Task<Product> UpdateProduct(Product newProduct)
    {
        _productRepository.Update(newProduct);
        await _persistence.Complete();
        return newProduct;
    }

    public async Task<Product> DeleteProduct(Product deleteProduct)
    {
        _productRepository.Delete(deleteProduct);
        await _persistence.Complete();
        return deleteProduct;
    }
}