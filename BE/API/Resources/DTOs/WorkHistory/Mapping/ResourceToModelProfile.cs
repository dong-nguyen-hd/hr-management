using API.Resources.DTOs.WorkHistory.Request;

namespace API.Resources.DTOs.WorkHistory.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<CreateRequest, Model.WorkHistory>()
            .ForMember(x => x.CompanyName, opt => opt.MapFrom(src => src.CompanyName.RemoveSpaceCharacter()))
            .ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position.RemoveSpaceCharacter()))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate.ConvertToDateonly()))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate.ConvertToDateonly()))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null && !string.IsNullOrEmpty(srcMember?.ToString())));

        CreateMap<UpdateRequest, Model.WorkHistory>()
            .ForMember(x => x.CompanyName, opt => opt.MapFrom(src => src.CompanyName.RemoveSpaceCharacter()))
            .ForMember(x => x.Position, opt => opt.MapFrom(src => src.Position.RemoveSpaceCharacter()))
            .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate.ConvertToDateonly()))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate.ConvertToDateonly()))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null && !string.IsNullOrEmpty(srcMember?.ToString())));
    }
}