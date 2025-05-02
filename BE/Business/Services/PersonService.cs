using AutoMapper;
using Business.Communication;
using Business.CustomException;
using Business.Domain.Models;
using Business.Domain.Repositories;
using Business.Domain.Services;
using Business.Extensions;
using Business.Resources;
using Business.Resources.Person;
using Business.Resources.Technology;
using Microsoft.Extensions.Options;

namespace Business.Services;

public class PersonService : BaseService<PersonResource, CreatePersonResource, UpdatePersonResource, Person>, IPersonService
{
    #region Constructor
    public PersonService(IPersonRepository personRepository,
        ITechnologyService technologyService,
        IPositionRepository officeRepository,
        IGroupRepository groupRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(personRepository, mapper, unitOfWork, responseMessage)
    {
        this._personRepository = personRepository;
        this._officeRepository = officeRepository;
        this._groupRepository = groupRepository;
        this._technologyService = technologyService;
    }
    #endregion

    #region Property
    private readonly IPersonRepository _personRepository;
    private readonly IPositionRepository _officeRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly ITechnologyService _technologyService;
    #endregion

    #region Method
    public override async Task<BaseResponse<PersonResource>> InsertAsync(CreatePersonResource createPersonResource)
    {
        try
        {
            // Validate Position is existent?
            var tempPosition = await _officeRepository.GetByIdAsync(createPersonResource.PositionId);
            if (tempPosition is null)
                return new BaseResponse<PersonResource>(ResponseMessage.Values["Position_NoData"]);

            // Validate Group is existent?
            if(createPersonResource.GroupId != null)
            {
                var tempGroup = await _groupRepository.GetByIdAsync((int)createPersonResource.GroupId);
                if (tempPosition is null)
                    return new BaseResponse<PersonResource>(ResponseMessage.Values["Group_NoData"]);
            }

            // Mapping Resource to Person
            var person = Mapper.Map<CreatePersonResource, Person>(createPersonResource);

            await _personRepository.InsertAsync(person);
            await UnitOfWork.CompleteAsync();

            // Mappping response
            var technologyResource = await _technologyService.GetAllAsync();
            var personResource = ConvertPersonResource(technologyResource.Resource, person);

            return new BaseResponse<PersonResource>(personResource);
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Person_Saving_Error"], ex);
        }
    }

    public override async Task<BaseResponse<PersonResource>> UpdateAsync(int id, UpdatePersonResource updatePersonResource)
    {
        try
        {
            // Validate Id is existent?
            var tempPerson = await _personRepository.GetByIdAsync(id);
            if (tempPerson is null)
                return new BaseResponse<PersonResource>(ResponseMessage.Values["Person_Id_NoData"]);
            // Validate Position is existent?
            var tempPosition = await _officeRepository.GetByIdAsync(updatePersonResource.PositionId);
            if (tempPosition is null)
                return new BaseResponse<PersonResource>(ResponseMessage.Values["Position_NoData"]);

            // Mapping Resource to Person
            Mapper.Map(updatePersonResource, tempPerson);

            await UnitOfWork.CompleteAsync();

            return new BaseResponse<PersonResource>(Mapper.Map<Person, PersonResource>(tempPerson));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Person_Saving_Error"], ex);
        }
    }

    public override async Task<BaseResponse<PersonResource>> GetByIdAsync(int id)
    {
        var totalTechnology = await _technologyService.GetAllAsync();
        var person = await _personRepository.GetByIdAsync(id);

        // Mapping
        var personResource = ConvertPersonResource(totalTechnology.Resource, person);

        return new BaseResponse<PersonResource>(personResource);
    }

    public async Task<PaginationResponse<IEnumerable<PersonResource>>> GetPaginationAsync(QueryResource pagination, FilterPersonResource filterResource)
    {
        var totalTechnology = await _technologyService.GetAllAsync();
        var paginationPerson = await _personRepository.GetPaginationAsync(pagination, filterResource);

        // Mapping
        var tempResource = ConvertPersonResource(totalTechnology.Resource, paginationPerson.records);

        var resource = new PaginationResponse<IEnumerable<PersonResource>>(tempResource);

        // Using extension-method for pagination
        resource.CreatePaginationResponse(pagination, paginationPerson.total);

        return resource;
    }

    public async Task<PaginationResponse<IEnumerable<PersonResource>>> GetPaginationAsync(QueryResource pagination, FilterPersonSalaryResource filterResource)
    {
        var paginationPerson = await _personRepository.GetPaginationWithSalaryAsync(pagination, filterResource);

        // Mapping
        var tempResource = Mapper.Map<IEnumerable<Person>, IEnumerable<PersonResource>>(paginationPerson.records);
        var resource = new PaginationResponse<IEnumerable<PersonResource>>(tempResource);

        // Using extension-method for pagination
        resource.CreatePaginationResponse(pagination, paginationPerson.total);

        return resource;
    }

    #region Private work
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