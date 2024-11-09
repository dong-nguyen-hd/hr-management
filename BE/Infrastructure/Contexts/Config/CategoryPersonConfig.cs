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
        entity.ToTable("tbl_categoryperson");

        entity.Property(x => x.CreatedDatetimeUtc).HasColumnType("timestamp without time zone");
        entity.Property(x => x.UpdatedDatetimeUtc).HasColumnType("timestamp without time zone");

        entity.HasKey(x => x.Id);
        entity.HasQueryFilter(x => x.Active);

        entity.HasIndex(x => x.Active);
    }
}