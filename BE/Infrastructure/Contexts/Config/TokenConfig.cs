using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Token table
/// </summary>
public class TokenConfig : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> entity)
    {
            entity.ToTable("Token");
            entity.Property(x => x.RefreshToken).IsRequired().HasColumnType("varchar(125)");
            entity.Property(x => x.ExpireTime).HasColumnType("datetime2");
            entity.Property(x => x.UserAgent).IsRequired().HasColumnType("nvarchar(500)");
            entity.HasIndex(x => x.ExpireTime);
        }
}