using AutoMapper;
using Business.Communication;
using Business.CustomException;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Resources;
using Business.Resources.Project;
using Microsoft.Extensions.Options;

namespace Business.Services;

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
    public override async Task<BaseResponse<ProjectResource>> InsertAsync(CreateProjectResource createProjectResource)
    {
        try
        {
            // Validate Group-Id is existent?
            var tempGroup = await _groupRepository.GetByIdAsync(createProjectResource.GroupId);
            if (tempGroup is null)
                return new BaseResponse<ProjectResource>(ResponseMessage.Values["Project_NoData"]);

            var tempProject = Mapper.Map<CreateProjectResource, Project>(createProjectResource);

            tempProject.OrderIndex = await _projectRepository.MaximumOrderIndexAsync(createProjectResource.PersonId);

            await _projectRepository.InsertAsync(tempProject);
            await UnitOfWork.CompleteAsync();

            return new BaseResponse<ProjectResource>(Mapper.Map<Project, ProjectResource>(tempProject));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Project_Saving_Error"], ex);
        }
    }

    public override async Task<BaseResponse<ProjectResource>> UpdateAsync(int id, UpdateProjectResource updateProjectResource)
    {
        try
        {
            // Validate Group-Id is existent?
            var tempProject = await _projectRepository.GetByIdAsync(id);
            if (tempProject is null)
                return new BaseResponse<ProjectResource>(ResponseMessage.Values["Project_NoData"]);

            var tempGroup = await _groupRepository.GetByIdAsync(updateProjectResource.GroupId);
            if (tempGroup is null)
                return new BaseResponse<ProjectResource>(ResponseMessage.Values["Group_NoData"]);

            // Update infomation
            Mapper.Map(updateProjectResource, tempProject);

            await UnitOfWork.CompleteAsync();
            // Mapping
            var resource = Mapper.Map<Project, ProjectResource>(tempProject);

            return new BaseResponse<ProjectResource>(resource);
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Project_Updating_Error"], ex);
        }
    }
    #endregion
}