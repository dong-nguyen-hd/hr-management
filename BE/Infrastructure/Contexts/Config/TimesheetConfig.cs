using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Timesheet table
/// </summary>
public class TimesheetConfig : IEntityTypeConfiguration<Timesheet>
{
    public void Configure(EntityTypeBuilder<Timesheet> entity)
    {
            entity.ToTable("Timesheet");
            entity.Property(x => x.TotalWorkDay).HasColumnType("decimal(3,1)");
            entity.Property(x => x.WorkDay).HasColumnType("decimal(3,1)");
            entity.Property(x => x.Absent).HasColumnType("decimal(3,1)");
            entity.Property(x => x.Holiday).HasColumnType("decimal(3,1)");
            entity.Property(x => x.UnpaidLeave).HasColumnType("decimal(3,1)");
            entity.Property(x => x.PaidLeave).HasColumnType("decimal(3,1)");
            entity.Property(x => x.TimesheetJSON).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.Date).HasColumnType("date");
            entity.HasIndex(x => x.Date);
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}