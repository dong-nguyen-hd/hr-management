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
        entity.ToTable("tbl_group");

        entity.Property(x => x.CreatedDatetimeUtc).HasColumnType("timestamp without time zone");
        entity.Property(x => x.UpdatedDatetimeUtc).HasColumnType("timestamp without time zone");

        entity.HasKey(x => x.Id);
        entity.HasQueryFilter(x => x.Active);

        entity.HasIndex(x => new { x.Name, x.Active });
    }
}