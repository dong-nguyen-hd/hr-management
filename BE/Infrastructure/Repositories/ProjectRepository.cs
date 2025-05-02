using Business.Domain.Models;
using Business.Domain.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    #region Constructor
    public ProjectRepository(AppDbContext context) : base(context) { }
    #endregion

    #region Method
    public override async Task<Project> GetByIdAsync(int id) =>
        await Context.Projects
            .Include(y => y.Group)
            .SingleOrDefaultAsync(x => x.Id == id);
    #endregion
}