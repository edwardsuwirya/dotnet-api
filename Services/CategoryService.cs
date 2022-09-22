using MySimpleNetApi.Exceptions;
using MySimpleNetApi.Models;
using MySimpleNetApi.Repository;

namespace MySimpleNetApi.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPersistence _persistence;

    public CategoryService(ICategoryRepository categoryRepository, IPersistence persistence)
    {
        _categoryRepository = categoryRepository;
        _persistence = persistence;
    }

    public Task<List<Category>> List()
    {
        return _categoryRepository.GetAll();
    }

    public async Task<Category> RegisterCategory(Category category)
    {
        try
        {
            await _categoryRepository.Save(category);
            await _persistence.Complete();
            return category;
        }
        catch (Exception e)
        {
            throw new DbException("Failed to register product");
        }
    }
}