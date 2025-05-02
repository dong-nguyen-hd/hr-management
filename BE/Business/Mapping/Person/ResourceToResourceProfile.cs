using AutoMapper;
using Business.Resources.Person;

namespace Business.Mapping.Person;

public class ResourceToResourceProfile : Profile
{
    public ResourceToResourceProfile()
    {
            CreateMap<PersonResource, PersonResourceView>();
        }
}