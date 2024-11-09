using Business.Domain.Models;
using Business.Resources.DTOs.Education;

namespace Business.Domain.Services;

public interface IEducationService : IBaseService<EducationResource, CreateEducationResource, UpdateEducationResource, Education>
{
}