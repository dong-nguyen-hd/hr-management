using API.Extensions;
using API.Resources.DTOs.WorkHistory;

namespace API.Mapping.WorkHistory;

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