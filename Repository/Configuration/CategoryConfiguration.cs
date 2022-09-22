using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySimpleNetApi.Models;

namespace MySimpleNetApi.Repository.Configuration;

/*
 * Class ini akan di-scan otomatis jika
 * Conventions Class Name {EntityName}Configuration
 * Extends IEntityTypeConfiguration<EntityName>
 */
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("M_CATEGORY");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.CategoryName).IsRequired().HasMaxLength(75);
        ;
        builder.HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);
    }
}