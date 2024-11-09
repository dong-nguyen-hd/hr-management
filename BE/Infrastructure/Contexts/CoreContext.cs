using Business.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Contexts;

public class CoreContext : DbContext
{
    #region Constructor
    public CoreContext(DbContextOptions<CoreContext> options) : base(options) { }
    #endregion

    #region Property
    public DbSet<Education> Educations { get; set; }
    public DbSet<WorkHistory> WorkHistories { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<CategoryPerson> CategoryPersons { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<RefreshToken> Tokens { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Pay> Pays { get; set; }
    public DbSet<Timesheet> Timesheets { get; set; }
    #endregion

    #region Method
    // Use Fluent API
    protected override void OnModelCreating(ModelBuilder builder)
    {
            base.OnModelCreating(builder);

            // Finds and runs all your configuration classes in the same assembly as the DbContext
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    #endregion
}