using AutoMapper;
using Business.Extensions;
using Business.Resources.WorkHistory;

namespace Business.Mapping.WorkHistory;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreateWorkHistoryResource, Domain.Models.WorkHistory>()
                .ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position.RemoveSpaceCharacter()))
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(src => src.CompanyName.RemoveSpaceCharacter()));

            CreateMap<UpdateWorkHistoryResource, Domain.Models.WorkHistory>()
                .ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position.RemoveSpaceCharacter()))
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(src => src.CompanyName.RemoveSpaceCharacter()));
        }
}