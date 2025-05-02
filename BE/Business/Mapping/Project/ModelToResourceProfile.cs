using AutoMapper;
using Business.Resources.Project;

namespace Business.Mapping.Project;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Project, ProjectResource>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Group.Name))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Group.Description))
                .ForMember(x => x.TeamSize, opt => opt.MapFrom(src => src.Group.TeamSize))
                .ForMember(x => x.GroupId, opt => opt.MapFrom(src => src.Group.Id))
                .ForMember(x => x.Technologies, opt => opt.Ignore());
        }
}