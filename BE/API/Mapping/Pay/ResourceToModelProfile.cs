using API.Resources.DTOs.Pay;

namespace API.Mapping.Pay;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
            CreateMap<CreatePayResource, Domain.Models.Pay>();

            CreateMap<UpdatePayResource, Domain.Models.Pay>();
        }
}