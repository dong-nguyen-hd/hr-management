using AutoMapper;
using Business.Resources.WorkHistory;

namespace Business.Mapping.WorkHistory;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.WorkHistory, WorkHistoryResource>();
        }
}