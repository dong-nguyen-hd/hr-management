using Business.Domain.Models;
using Business.Extensions;
using Business.Resources.Enums;
using Business.Resources.SystemData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Account table
/// </summary>
public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public const string AdminId = "-1";
    
    public void Configure(EntityTypeBuilder<Account> entity)
    {
        entity.ToTable("tbl_account");

        entity.Property(x => x.CreatedDatetimeUtc).HasColumnType("timestamp without time zone");
        entity.Property(x => x.UpdatedDatetimeUtc).HasColumnType("timestamp without time zone");

        var valueComparer = new ValueComparer<List<string>>(
            (c1, c2) => c1.SequenceEqual(c2),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList());

        entity.Property(x => x.SystemRoles).HasConversion(
            v => v.MySerialize(),
            v => v.MyDeserialize<List<string>>(),
            valueComparer);

        entity.HasKey(x => x.Id);
        entity.HasQueryFilter(x => x.Active);

        // Indexing
        entity.HasIndex(x => x.UserName).IsUnique();
        entity.HasIndex(x => new { x.UserName, x.Active })
            .IncludeProperties(x =>
                new
                {
                    x.Id,
                    x.Password,
                });

        // Seeding data for Account table
        entity.HasData(
            new Account
            {
                Id = AdminId,
                UserName = "admin",
                Password = "10000.om7b7+wyo6oufN1g+bOnKQ==.3WbZ3GlZH6mz6JPohiNcH0UI0OmssZA9h/XeoodmDRY=", // Input: c93ccd78b2076528346216b3b2f701e6 (plain-text: admin1234) (hash function: MD5)
                Email = "dong.nguyen.hdkt@gmail.com",
                SystemRoles = [MyPolicy.Administrator],
                Name = "Dong Nguyen",
                Avatar = Constant.DefaultAvatar,
                CreatedDatetimeUtc = DateTime.UtcNow,
                UpdatedDatetimeUtc = DateTime.UtcNow,
                Active = true
            }
        );
    }
}