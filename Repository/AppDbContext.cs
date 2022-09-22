using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MySimpleNetApi.Models;

namespace MySimpleNetApi.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Scan otomatis ke class Entity Configuration (CategoryConfiguration & ProductConfiguration)
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}