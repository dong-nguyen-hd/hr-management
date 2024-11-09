using AutoMapper;
using Business.Extensions;
using Business.Resources.DTOs.Education;

namespace Business.Mapping.Education;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreateEducationResource, Domain.Models.Education>()
                .ForMember(x => x.CollegeName, opt => opt.MapFrom(src => src.CollegeName.RemoveSpaceCharacter()))
                .ForMember(x => x.Major, opt => opt.MapFrom(src => src.Major.RemoveSpaceCharacter()));

            CreateMap<UpdateEducationResource, Domain.Models.Education>()
                .ForMember(x => x.CollegeName, opt => opt.MapFrom(src => src.CollegeName.RemoveSpaceCharacter()))
                .ForMember(x => x.Major, opt => opt.MapFrom(src => src.Major.RemoveSpaceCharacter()));
        }
}