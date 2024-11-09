using API.Resources.DTOs.Group;

namespace API.Mapping.Group;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Group, GroupResource>()
                .ForMember(x => x.IsFinished, opt => opt.MapFrom(src => src.EndDate == null ? false : true))
                .ForMember(x => x.Technologies, opt => opt.Ignore());
        }
}