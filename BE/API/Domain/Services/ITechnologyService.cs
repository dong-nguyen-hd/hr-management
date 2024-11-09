using API.Domain.Models;
using API.Resources.DTOs.Technology;

namespace API.Domain.Services;

public interface ITechnologyService : IBaseService<TechnologyResource, CreateTechnologyResource, UpdateTechnologyResource, Technology>
{
}