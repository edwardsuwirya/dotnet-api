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
        try
        {
            await _productRepository.Save(product);
            await _persistence.Complete();
            return product;
        }
        catch (Exception e)
        {
            throw new DbException("Failed to register product");
        }
    }

    public async Task<Product> UpdateProduct(string id, Product product)
    {
        try
        {
            var existingProduct = await _productRepository.FindById(id);
            if (existingProduct == null)
                throw new NotFoundException("Product not found.");
            existingProduct.ProductName = product.ProductName;
            _productRepository.Update(existingProduct);
            await _persistence.Complete();
            return existingProduct;
        }
        catch (Exception e)
        {
            throw new DbException("Failed to update product");
        }
    }

    public async Task<Product> DeleteProduct(string id)
    {
        try
        {
            var existingProduct = await _productRepository.FindById(id);
            if (existingProduct == null)
                throw new NotFoundException("Product not found.");
            _productRepository.Delete(existingProduct);
            await _persistence.Complete();
            return existingProduct;
        }
        catch (Exception e)
        {
            throw new DbException("Failed to delete product");
        }
    }
}