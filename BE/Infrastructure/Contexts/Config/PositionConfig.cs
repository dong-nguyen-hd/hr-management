using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Position table
/// </summary>
public class PositionConfig : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> entity)
    {
            entity.ToTable("Position");
            entity.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(500)");
            entity.HasIndex(x => x.Name);
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}