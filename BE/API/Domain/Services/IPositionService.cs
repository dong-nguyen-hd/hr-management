using API.Domain.Models;
using API.Resources.DTOs.Position;

namespace API.Domain.Services;

public interface IPositionService : IBaseService<PositionResource, CreatePositionResource, UpdatePositionResource, Position>
{
}