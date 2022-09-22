namespace MySimpleNetApi.Repository;

public class DbPersistence : IPersistence
{
    private readonly AppDbContext _context;

    public DbPersistence(AppDbContext context) => _context = context;

    public async Task Complete()
    {
        await _context.SaveChangesAsync();
    }
}