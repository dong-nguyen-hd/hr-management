using AutoMapper;
using Business.Resources.DTOs.Timesheet;

namespace Business.Mapping.Timesheet;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
            CreateMap<Domain.Models.Timesheet, TimesheetResource>();
        }
}