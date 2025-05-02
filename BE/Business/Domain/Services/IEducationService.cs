using Business.Domain.Models;
using Business.Resources.Education;

namespace Business.Domain.Services;

public interface IEducationService : IBaseService<EducationResource, CreateEducationResource, UpdateEducationResource, Education>
{
}