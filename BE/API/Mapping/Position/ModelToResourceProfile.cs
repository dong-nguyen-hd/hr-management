using API.Resources.DTOs.Position;

namespace API.Mapping.Position;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Position, PositionResource>();
        }
}