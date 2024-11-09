using Business.Domain.Models;
using Business.Extensions;
using Business.Resources.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Account table
/// </summary>
public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> entity)
    {
            entity.ToTable("account");
            
            entity.Property(x => x.UserName).IsRequired().HasColumnType("varchar(125)");
            entity.Property(x => x.Password).IsRequired().HasColumnType("varchar(125)");
            entity.Property(x => x.Name).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.Email).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.Role).IsRequired().HasColumnType("varchar(50)");
            entity.Property(x => x.CreatedAt).IsRequired().HasColumnType("datetime2");
            entity.Property(x => x.LastActivity).IsRequired().HasColumnType("datetime2");
            entity.HasIndex(x => x.UserName);
            entity.HasQueryFilter(x => !x.IsDeleted);

            // Seeding data for Account table
            entity.HasData(
                new Account
                {
                    Id = -1,
                    UserName = "admin",
                    Password = "10000.om7b7+wyo6oufN1g+bOnKQ==.3WbZ3GlZH6mz6JPohiNcH0UI0OmssZA9h/XeoodmDRY=", // Input: c93ccd78b2076528346216b3b2f701e6 (plain-text: admin1234) (hash function: MD5)
                    Email = "dong.nguyen.hdkt@gmail.com",
                    Role = eRole.Admin.ToDescriptionString(),
                    CreatedAt = DateTime.UtcNow,
                    Name = "Dong Nguyen",
                    LastActivity = DateTime.UtcNow,
                    IsDeleted = false,
                    Avatar = Constant.DefaultAvatar
                }
            );
        }
}