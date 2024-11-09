using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Token table
/// </summary>
public class TokenConfig : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> entity)
    {
            entity.ToTable("tbl_refresh_token");
            
            entity.Property(x => x.ExpireDatetimeUtc).HasColumnType("timestamp without time zone");
            entity.Property(x => x.CreatedDatetimeUtc).HasColumnType("timestamp without time zone");
            entity.Property(x => x.UpdatedDatetimeUtc).HasColumnType("timestamp without time zone");
            
            entity.HasKey(x => x.Id);
            
            // Indexing
            entity.HasIndex(x => new { x.ExpireDatetimeUtc, x.IsUsed, x.Active });
        }
}