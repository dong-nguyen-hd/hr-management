using API.Domain.Models;
using API.Resources.DTOs.Education;

namespace API.Domain.Services;

public interface IEducationService : IBaseService<EducationResource, CreateEducationResource, UpdateEducationResource, Education>
{
}