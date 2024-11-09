using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Group table
/// </summary>
public class GroupConfig : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> entity)
    {
            entity.ToTable("Group");
            entity.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.Description).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.Technologies).IsRequired().HasColumnType("varchar(500)");
            entity.Property(x => x.StartDate).IsRequired().HasColumnType("date");
            entity.Property(x => x.EndDate).HasColumnType("date");
            entity.HasIndex(x => x.Name);
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}