using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Certificate table
/// </summary>
public class CertificateConfig : IEntityTypeConfiguration<Certificate>
{
    public void Configure(EntityTypeBuilder<Certificate> entity)
    {
            entity.ToTable("Certificate");
            entity.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.Provider).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.StartDate).IsRequired().HasColumnType("date");
            entity.Property(x => x.EndDate).HasColumnType("date");
            entity.HasIndex(x => x.Name);
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}