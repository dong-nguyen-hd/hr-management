using API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Domain.Contexts.Config;

/// <summary>
/// Setting schema for Timesheet table
/// </summary>
public class TimesheetConfig : IEntityTypeConfiguration<Timesheet>
{
    public void Configure(EntityTypeBuilder<Timesheet> entity)
    {
        entity.ToTable("tbl_timesheet");

        entity.Property(x => x.CreatedDatetimeUtc).HasColumnType("timestamp without time zone");
        entity.Property(x => x.UpdatedDatetimeUtc).HasColumnType("timestamp without time zone");

        entity.HasKey(x => x.Id);

        entity.HasQueryFilter(x => x.Active);
        entity.HasIndex(x => new { x.Date, x.Active });
    }
}