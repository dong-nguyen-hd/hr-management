using API.Resources.DTOs.Person;

namespace API.Mapping.Person;

public class ResourceToResourceProfile : Profile
{
    public ResourceToResourceProfile()
    {
            CreateMap<PersonResource, PersonResourceView>();
        }
}