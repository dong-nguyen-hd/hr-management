using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.Project;
using API.Results;
using Microsoft.Extensions.Options;

namespace API.Services;

public class ProjectService : BaseService<ProjectResource, CreateProjectResource, UpdateProjectResource, Project>, IProjectService
{
    #region Constructor
    public ProjectService(IProjectRepository projectRepository,
        IGroupRepository groupRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(projectRepository, mapper, unitOfWork, responseMessage)
    {
        this._projectRepository = projectRepository;
        this._groupRepository = groupRepository;
    }
    #endregion

    #region Property
    private readonly IProjectRepository _projectRepository;
    private readonly IGroupRepository _groupRepository;
    #endregion

    #region Method
    public override async Task<BaseResult<ProjectResource>> InsertAsync(CreateProjectResource createProjectResource)
    {
        try
        {
            // Validate Group-Id is existent?
            var tempGroup = await _groupRepository.GetByIdAsync(createProjectResource.GroupId);
            if (tempGroup is null)
                return new BaseResult<ProjectResource>(ResponseMessage.Values["Project_NoData"]);

            var tempProject = Mapper.Map<CreateProjectResource, Project>(createProjectResource);

            tempProject.OrderIndex = await _projectRepository.MaximumOrderIndexAsync(createProjectResource.PersonId);

            await _projectRepository.InsertAsync(tempProject);
            await UnitOfWork.CompleteAsync();

            return new BaseResult<ProjectResource>(Mapper.Map<Project, ProjectResource>(tempProject));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Project_Saving_Error"], ex);
        }
    }

    public override async Task<BaseResult<ProjectResource>> UpdateAsync(int id, UpdateProjectResource updateProjectResource)
    {
        try
        {
            // Validate Group-Id is existent?
            var tempProject = await _projectRepository.GetByIdAsync(id);
            if (tempProject is null)
                return new BaseResult<ProjectResource>(ResponseMessage.Values["Project_NoData"]);

            var tempGroup = await _groupRepository.GetByIdAsync(updateProjectResource.GroupId);
            if (tempGroup is null)
                return new BaseResult<ProjectResource>(ResponseMessage.Values["Group_NoData"]);

            // Update infomation
            Mapper.Map(updateProjectResource, tempProject);

            await UnitOfWork.CompleteAsync();
            // Mapping
            var resource = Mapper.Map<Project, ProjectResource>(tempProject);

            return new BaseResult<ProjectResource>(resource);
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Project_Updating_Error"], ex);
        }
    }
    #endregion
}