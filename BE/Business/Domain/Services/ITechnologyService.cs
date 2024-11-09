using Business.Domain.Models;
using Business.Resources.DTOs.Technology;

namespace Business.Domain.Services;

public interface ITechnologyService : IBaseService<TechnologyResource, CreateTechnologyResource, UpdateTechnologyResource, Technology>
{
}