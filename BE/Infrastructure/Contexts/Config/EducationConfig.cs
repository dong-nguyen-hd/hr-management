using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Education table
/// </summary>
public class EducationConfig : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> entity)
    {
            entity.ToTable("Education");
            entity.Property(x => x.CollegeName).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.Major).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.StartDate).IsRequired().HasColumnType("date");
            entity.Property(x => x.EndDate).HasColumnType("date");
            entity.HasIndex(x => x.CollegeName);
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}