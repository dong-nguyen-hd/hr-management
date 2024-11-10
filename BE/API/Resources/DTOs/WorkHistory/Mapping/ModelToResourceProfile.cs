using API.Resources.DTOs.WorkHistory.Response;

namespace API.Resources.DTOs.WorkHistory.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Domain.Models.WorkHistory, WorkHistoryResponse>()
            .ForMember(x => x.StartDate, opt => opt.MapFrom(src => src.StartDate.ConvertToDatetime()))
            .ForMember(x => x.EndDate, opt => opt.MapFrom(src => src.EndDate.ConvertToDatetime()))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null && !string.IsNullOrEmpty(srcMember?.ToString())));
    }
}