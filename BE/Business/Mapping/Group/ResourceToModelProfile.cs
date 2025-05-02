using AutoMapper;
using Business.Extensions;
using Business.Resources.Group;

namespace Business.Mapping.Group;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreateGroupResource, Domain.Models.Group>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description.RemoveSpaceCharacter()))
                .ForMember(x => x.Technologies, opt => opt.MapFrom(src => src.Technologies.ConcatenateWithComma()));

            CreateMap<UpdateGroupResource, Domain.Models.Group>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name.RemoveSpaceCharacter()))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Description.RemoveSpaceCharacter()))
                .ForMember(x => x.Technologies, opt => opt.MapFrom(src => src.Technologies.ConcatenateWithComma()));
        }
}