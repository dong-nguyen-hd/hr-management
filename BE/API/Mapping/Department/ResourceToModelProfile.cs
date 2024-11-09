using API.Extensions;
using API.Resources.DTOs.Department;

namespace API.Mapping.Department;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreateDepartmentResource, Domain.Models.Department>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()));

            CreateMap<UpdateDepartmentResource, Domain.Models.Department>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()));
        }
}