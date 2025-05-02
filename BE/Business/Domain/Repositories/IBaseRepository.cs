using System.Linq.Expressions;

namespace Business.Domain.Repositories;

public interface IBaseRepository<Entity> where Entity : class
{
    Task<Entity> GetByIdAsync(int entityId);
    Task<IEnumerable<Entity>> GetWithPrimaryKeyAsync(List<int> keyValues);
    Task InsertAsync(Entity entity);
    Task AddRangeAsync(IEnumerable<Entity> entities);
    void AttachRange(IEnumerable<Entity> entities);
    void Remove(Entity entity);
    int RemoveRange(IEnumerable<Entity> entities);
    void Update(Entity entity);
    Task<int> MaximumOrderIndexAsync(int personId);
    Task<IEnumerable<Entity>> GetAllAsync();
    Task<IEnumerable<Entity>> FindAsync(Expression<Func<Entity, bool>> expression);
}