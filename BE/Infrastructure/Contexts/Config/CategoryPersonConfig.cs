using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Category-Person table
/// </summary>
public class CategoryPersonConfig : IEntityTypeConfiguration<CategoryPerson>
{
    public void Configure(EntityTypeBuilder<CategoryPerson> entity)
    {
            entity.ToTable("CategoryPerson");
            entity.Property(x => x.Technologies).IsRequired().HasColumnType("varchar(500)");
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}