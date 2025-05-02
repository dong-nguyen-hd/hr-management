using Business.Domain.Models;
using Business.Resources.Project;

namespace Business.Domain.Services;

public interface IProjectService : IBaseService<ProjectResource, CreateProjectResource, UpdateProjectResource, Project>
{
}