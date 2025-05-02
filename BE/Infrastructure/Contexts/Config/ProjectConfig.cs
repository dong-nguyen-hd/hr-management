using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Project table
/// </summary>
public class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> entity)
    {
            entity.ToTable("Project");
            entity.Property(x => x.Position).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.Responsibilities).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.StartDate).IsRequired().HasColumnType("date");
            entity.Property(x => x.EndDate).HasColumnType("date");
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}