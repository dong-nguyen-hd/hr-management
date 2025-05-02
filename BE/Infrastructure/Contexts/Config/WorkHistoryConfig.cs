using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for WorkHistory table
/// </summary>
public class WorkHistoryConfig : IEntityTypeConfiguration<WorkHistory>
{
    public void Configure(EntityTypeBuilder<WorkHistory> entity)
    {
            entity.ToTable("WorkHistory");
            entity.Property(x => x.Position).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.CompanyName).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.StartDate).IsRequired().HasColumnType("date");
            entity.Property(x => x.EndDate).HasColumnType("date");
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}