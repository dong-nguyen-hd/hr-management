using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Pay table
/// </summary>
public class PayConfig : IEntityTypeConfiguration<Pay>
{
    public void Configure(EntityTypeBuilder<Pay> entity)
    {
            entity.ToTable("Pay");
            entity.Property(x => x.BaseSalary).HasColumnType("money");
            entity.Property(x => x.Allowance).HasColumnType("money");
            entity.Property(x => x.Bonus).HasColumnType("money");
            entity.Property(x => x.PIT).HasColumnType("real");
            entity.Property(x => x.SocialInsurance).HasColumnType("real");
            entity.Property(x => x.HealthInsurance).HasColumnType("real");
            entity.Property(x => x.Date).HasColumnType("date");
            entity.HasIndex(x => x.Date);
            entity.Property(x => x.TotalWorkDay).HasColumnType("decimal(3,1)");
            entity.Property(x => x.WorkDay).HasColumnType("decimal(3,1)");
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}