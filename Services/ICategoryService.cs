using MySimpleNetApi.Models;

namespace MySimpleNetApi.Services;

public interface ICategoryService
{
    Task<List<Category>> List();
    Task<Category> RegisterCategory(Category category);
}