using API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Domain.Contexts.Config;

/// <summary>
/// Setting schema for WorkHistory table
/// </summary>
public class WorkHistoryConfig : IEntityTypeConfiguration<WorkHistory>
{
    public void Configure(EntityTypeBuilder<WorkHistory> entity)
    {
            entity.ToTable("tbl_work_history");
            
            entity.Property(x => x.StartDate).HasColumnType("timestamp without time zone");
            entity.Property(x => x.EndDate).HasColumnType("timestamp without time zone");
            
            entity.Property(x => x.CreatedDatetimeUtc).HasColumnType("timestamp without time zone");
            entity.Property(x => x.UpdatedDatetimeUtc).HasColumnType("timestamp without time zone");
           
            entity.HasKey(x => x.Id);
            entity.HasQueryFilter(x => x.Active);
        }
}