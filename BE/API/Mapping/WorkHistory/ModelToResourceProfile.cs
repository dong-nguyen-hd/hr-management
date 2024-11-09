using API.Resources.DTOs.WorkHistory;

namespace API.Mapping.WorkHistory;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.WorkHistory, WorkHistoryResource>();
        }
}