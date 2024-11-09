using API.Domain.Models;
using API.Domain.Services;
using API.Resources.DTOs.Department;
using API.Results;
using Microsoft.Extensions.Options;

namespace API.Services;

public class DepartmentService : BaseService<DepartmentResource, CreateDepartmentResource, UpdateDepartmentResource, Department>, IDepartmentService
{
    #region Property
    private readonly IDepartmentRepository _departmentRepository;
    #endregion

    #region Constructor
    public DepartmentService(IDepartmentRepository departmentRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOptionsMonitor<ResponseMessage> responseMessage) : base(departmentRepository, mapper, unitOfWork, responseMessage)
    {
        this._departmentRepository = departmentRepository;
    }
    #endregion

    #region Method
    public override async Task<BaseResult<DepartmentResource>> InsertAsync(CreateDepartmentResource createDepartmentResource)
    {
        try
        {
            // Validate department name is existent?
            var hasValue = await _departmentRepository.FindByNameAsync(createDepartmentResource.Name, true);
            if (hasValue.Count > 0)
                return new BaseResult<DepartmentResource>(ResponseMessage.Values["Department_Existent"]);

            // Mapping Resource to Department
            var department = Mapper.Map<CreateDepartmentResource, Department>(createDepartmentResource);

            await _departmentRepository.InsertAsync(department);
            await UnitOfWork.CompleteAsync();

            return new BaseResult<DepartmentResource>(Mapper.Map<Department, DepartmentResource>(department));
        }
        catch (Exception ex)
        {
            throw new MessageResultException(ResponseMessage.Values["Department_Saving_Error"], ex);
        }
    }
    #endregion
}