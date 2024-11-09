using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Department table
/// </summary>
public class DepartmentConfig : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> entity)
    {
            entity.ToTable("Department");
            entity.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(500)");
            entity.HasIndex(x => x.Name);
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}