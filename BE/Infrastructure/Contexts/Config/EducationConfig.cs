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
        entity.ToTable("tbl_education");
        
        entity.Property(x => x.CreatedDatetimeUtc).HasColumnType("timestamp without time zone");
        entity.Property(x => x.UpdatedDatetimeUtc).HasColumnType("timestamp without time zone");

        entity.HasKey(x => x.Id);
        entity.HasQueryFilter(x => x.Active);

        entity.HasIndex(x => new { x.CollegeName, x.Active });
    }
}