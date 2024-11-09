using AutoMapper;
using Business.Resources.DTOs.Person;

namespace Business.Mapping.Person;

public class ResourceToResourceProfile : Profile
{
    public ResourceToResourceProfile()
    {
            CreateMap<PersonResource, PersonResourceView>();
        }
}