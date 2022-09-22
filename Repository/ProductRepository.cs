using Microsoft.EntityFrameworkCore;
using MySimpleNetApi.Models;

namespace MySimpleNetApi.Repository;

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public Task<List<Product>> GetAll()
    {
        return _context.Products.ToListAsync();
    }

    public async Task<Product?> FindById(string id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task Save(Product product)
    {
        product.Id = Guid.NewGuid().ToString();
        await _context.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }
}