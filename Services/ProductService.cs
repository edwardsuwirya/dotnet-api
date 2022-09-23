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

    public async Task<Product> UpdateProduct(string id, Product product)
    {
        var existingProduct = await _productRepository.FindById(id);
        if (existingProduct == null)
            throw new NotFoundException("Product not found.");
        existingProduct.ProductName = product.ProductName;
        _productRepository.Update(existingProduct);
        await _persistence.Complete();
        return existingProduct;
    }

    public async Task<Product> DeleteProduct(string id)
    {
        var existingProduct = await _productRepository.FindById(id);
        if (existingProduct == null)
            throw new NotFoundException("Product not found.");
        _productRepository.Delete(existingProduct);
        await _persistence.Complete();
        return existingProduct;
    }
}