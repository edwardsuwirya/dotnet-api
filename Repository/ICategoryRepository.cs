using MySimpleNetApi.Models;

namespace MySimpleNetApi.Repository;

public interface ICategoryRepository
{
    Task<List<Category>> GetAll();
    Task<Category?> FindById(string id);
    Task Save(Category product);
    void Update(Category product);
    void Delete(Category product);
}