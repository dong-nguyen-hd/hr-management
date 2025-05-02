using AutoMapper;
using Business.Resources.Position;

namespace Business.Mapping.Position;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Position, PositionResource>();
        }
}