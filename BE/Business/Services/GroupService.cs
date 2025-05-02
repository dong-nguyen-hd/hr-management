using AutoMapper;
using Business.Communication;
using Business.CustomException;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Extensions;
using Business.Resources;
using Business.Resources.Group;
using Business.Resources.Person;
using Business.Resources.Technology;
using Microsoft.Extensions.Options;

namespace Business.Services;

public class GroupService : BaseService<GroupResource, CreateGroupResource, UpdateGroupResource, Group>, IGroupService
{
    #region Constructor
    public GroupService(IGroupRepository groupRepository,
        IAccountRepository _accountRepository,
        ITechnologyService technologyService,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(groupRepository, mapper, unitOfWork, responseMessage)
    {
        this._groupRepository = groupRepository;
        this._accountRepository = _accountRepository;
        this._technologyService = technologyService;
    }
    #endregion

    #region Property
    private readonly IGroupRepository _groupRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ITechnologyService _technologyService;
    #endregion

    #region Method
    public async Task<BaseResponse<GroupResource>> AddGroupToAccountAsync(int accountId, int groupId)
    {
        try
        {
            // Validate account-Id is existent?
            var tempAccount = await _accountRepository.GetByIdAsync(accountId);
            if (tempAccount is null)
                return new BaseResponse<GroupResource>(ResponseMessage.Values["Account_NoData"]);

            // Validate group-Id is existent?
            var tempGroup = await _groupRepository.GetByIdAsync(groupId);
            if (tempGroup is null)
                return new BaseResponse<GroupResource>(ResponseMessage.Values["Group_NoData"]);


            tempAccount.Groups = new() { tempGroup };
            await UnitOfWork.CompleteAsync();

            return new BaseResponse<GroupResource>(Mapper.Map<Group, GroupResource>(tempGroup));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Group_Saving_Error"], ex);
        }
    }

    public async Task<BaseResponse<IEnumerable<PersonResource>>> GetListPersonByGroupIdAsync(int groupId)
    {
        var totalTechnology = await _technologyService.GetAllAsync();
        var people = await _groupRepository.GetListPersonByGroupIdAsync(groupId);

        // Mapping
        var personResource = ConvertPersonResource(totalTechnology.Resource, people);

        return new BaseResponse<IEnumerable<PersonResource>>(personResource);
    }

    public async Task<BaseResponse<GroupResource>> RemoveGroupFromAccountAsync(int accountId, int groupId)
    {
        try
        {
            // Validate account-Id is existent?
            var tempAccount = await _accountRepository.GetByIdIncludeGroupAsync(accountId, true);
            if (tempAccount is null)
                return new BaseResponse<GroupResource>(ResponseMessage.Values["Account_NoData"]);

            // Validate group-Id is existent?
            var tempGroup = await _groupRepository.GetByIdAsync(groupId);
            if (tempGroup is null)
                return new BaseResponse<GroupResource>(ResponseMessage.Values["Group_NoData"]);

            tempAccount.Groups.Remove(tempGroup);
            await UnitOfWork.CompleteAsync();

            return new BaseResponse<GroupResource>(Mapper.Map<Group, GroupResource>(tempGroup));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Group_Saving_Error"], ex);
        }
    }

    public override async Task<BaseResponse<GroupResource>> InsertAsync(CreateGroupResource createGroupResource)
    {
        try
        {
            var tempGroup = Mapper.Map<CreateGroupResource, Group>(createGroupResource);

            await _groupRepository.InsertAsync(tempGroup);
            await UnitOfWork.CompleteAsync();

            // Mapping
            var totalTechnology = await _technologyService.GetAllAsync();
            var tempResource = ConvertGroupResource(totalTechnology.Resource, tempGroup);

            return new BaseResponse<GroupResource>(tempResource);
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Group_Saving_Error"], ex);
        }
    }

    public override async Task<BaseResponse<GroupResource>> UpdateAsync(int id, UpdateGroupResource updateGroupResource)
    {
        try
        {
            // Validate Id is existent?
            var tempGroup = await _groupRepository.GetByIdAsync(id);
            if (tempGroup is null)
                return new BaseResponse<GroupResource>(ResponseMessage.Values["Group_NoData"]);

            // Update infomation
            Mapper.Map(updateGroupResource, tempGroup);
            await UnitOfWork.CompleteAsync();

            // Mapping
            var totalTechnology = await _technologyService.GetAllAsync();
            var tempResource = ConvertGroupResource(totalTechnology.Resource, tempGroup);

            return new BaseResponse<GroupResource>(tempResource);
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Group_Updating_Error"], ex);
        }
    }

    public async Task<PaginationResponse<IEnumerable<GroupResource>>> GetPaginationAsync(QueryResource pagination, FilterGroupResource filterResource)
    {
        var totalTechnology = await _technologyService.GetAllAsync();
        var paginationPerson = await _groupRepository.GetPaginationAsync(pagination, filterResource);

        // Mapping
        var tempResource = ConvertGroupResource(totalTechnology.Resource, paginationPerson.records);

        var resource = new PaginationResponse<IEnumerable<GroupResource>>(tempResource);

        // Using extension-method for pagination
        resource.CreatePaginationResponse(pagination, paginationPerson.total);

        return resource;
    }

    #region Private work
    private IEnumerable<GroupResource> ConvertGroupResource(IEnumerable<TechnologyResource> totalTechnology, IEnumerable<Group> totalGroup)
    {
        List<GroupResource> listGroupResource = new List<GroupResource>(totalGroup.Count());

        foreach (var group in totalGroup)
        {
            var tempGroupResource = ConvertGroupResource(totalTechnology, group);

            listGroupResource.Add(tempGroupResource);
        }

        return listGroupResource;
    }

    private GroupResource ConvertGroupResource(IEnumerable<TechnologyResource> totalTechnology, Group group)
    {
        var tempGroupResource = Mapper.Map<Group, GroupResource>(group);

        if (!string.IsNullOrEmpty(group.Technologies))
            tempGroupResource.Technologies = totalTechnology.IntersectTechnology(group.Technologies);

        return tempGroupResource;
    }

    private IEnumerable<PersonResource> ConvertPersonResource(IEnumerable<TechnologyResource> totalTechnology, IEnumerable<Person> totalPerson)
    {
        List<PersonResource> listPersonResource = new List<PersonResource>(totalPerson.Count());

        foreach (var person in totalPerson)
        {
            var tempPersonResource = ConvertPersonResource(totalTechnology, person);

            listPersonResource.Add(tempPersonResource);
        }

        return listPersonResource;
    }

    private PersonResource ConvertPersonResource(IEnumerable<TechnologyResource> totalTechnology, Person person)
    {
        var tempPersonResource = Mapper.Map<Person, PersonResource>(person);

        // Project mapping
        var listProject = person.Projects.ToList();
        var countProject = listProject.Count;
        for (int i = 0; i < countProject; i++)
            if (!string.IsNullOrEmpty(listProject?[i]?.Group?.Technologies))
                tempPersonResource.Project[i].Technologies = totalTechnology.IntersectTechnology(listProject[i]?.Group.Technologies);

        // Category-Person mapping
        var listCategoryPerson = person.CategoryPersons.ToList();
        var countCategoryPerson = listCategoryPerson.Count;
        for (int i = 0; i < countCategoryPerson; i++)
            if (!string.IsNullOrEmpty(listCategoryPerson?[i].Technologies))
                tempPersonResource.CategoryPerson[i].Technologies = totalTechnology.IntersectTechnology(listCategoryPerson[i].Technologies);

        return tempPersonResource;
    }
    #endregion

    #endregion
}