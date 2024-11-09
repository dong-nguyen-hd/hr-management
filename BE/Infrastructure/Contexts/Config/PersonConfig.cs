using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Config;

/// <summary>
/// Setting schema for Person table
/// </summary>
public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> entity)
    {
            entity.ToTable("Person");
            entity.Property(x => x.FirstName).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.LastName).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.Email).HasColumnType("nvarchar(500)");
            entity.Property(x => x.Avatar).HasColumnType("varchar(250)");
            entity.Property(x => x.Description).HasColumnType("nvarchar(500)");
            entity.Property(x => x.Phone).HasColumnType("varchar(25)");
            entity.Property(x => x.CreatedBy).IsRequired().HasColumnType("nvarchar(500)");
            entity.Property(x => x.CreatedAt).IsRequired().HasColumnType("datetime2");
            entity.Property(x => x.DateOfBirth).IsRequired().HasColumnType("date");
            entity.Property(x => x.OrderIndex).IsRequired().HasColumnType("varchar(250)");
            entity.Property(x => x.StaffId).IsRequired().HasColumnType("varchar(25)");
            entity.HasIndex(x => x.StaffId);
            entity.HasIndex(x => x.FirstName);
            entity.HasQueryFilter(x => !x.IsDeleted);
        }
}