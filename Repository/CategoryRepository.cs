using Microsoft.EntityFrameworkCore;
using MySimpleNetApi.Models;

namespace MySimpleNetApi.Repository;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<Category>> GetAll()
    {
        return _context.Categories.ToListAsync();
    }

    public async Task<Category?> FindById(string id)
    {
        return await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Save(Category category)
    {
        category.Id = Guid.NewGuid().ToString();
        await _context.Categories.AddAsync(category);
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
    }

    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
    }
}