using MySimpleNetApi.Exceptions;

namespace MySimpleNetApi.Repository;

public class DbPersistence : IPersistence
{
    private readonly AppDbContext _context;

    public DbPersistence(AppDbContext context) => _context = context;

    public async Task Complete()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new DbException("Failed to update database");
        }
    }
}