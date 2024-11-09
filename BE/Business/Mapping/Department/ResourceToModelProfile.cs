using AutoMapper;
using Business.Extensions;
using Business.Resources.DTOs.Department;

namespace Business.Mapping.Department;

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