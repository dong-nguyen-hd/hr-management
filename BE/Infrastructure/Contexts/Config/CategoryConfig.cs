using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Category table
/// </summary>
public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
            entity.ToTable("Category");
            entity.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(500)");
            entity.HasIndex(x => x.Name);
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}