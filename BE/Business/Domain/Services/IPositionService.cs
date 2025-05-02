using Business.Domain.Models;
using Business.Resources.Position;

namespace Business.Domain.Services;

public interface IPositionService : IBaseService<PositionResource, CreatePositionResource, UpdatePositionResource, Position>
{
}