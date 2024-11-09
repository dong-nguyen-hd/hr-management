using API.Resources.DTOs.Timesheet;

namespace API.Mapping.Timesheet;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Timesheet, TimesheetResource>();
        }
}