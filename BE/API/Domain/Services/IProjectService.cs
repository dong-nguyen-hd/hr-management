using API.Domain.Models;
using API.Resources.DTOs.Project;

namespace API.Domain.Services;

public interface IProjectService : IBaseService<ProjectResource, CreateProjectResource, UpdateProjectResource, Project>
{
}