using Business.Domain.Repositories;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
{
    #region Property
    protected readonly CoreContext Context;
    private DbSet<Entity> _entities;
    #endregion

    #region Constructor
    public BaseRepository(CoreContext context)
    {
            this.Context = context;
            this._entities = Context.Set<Entity>();
        }
    #endregion

    #region Method
    public virtual async Task<IEnumerable<Entity>> FindAsync(Expression<Func<Entity, bool>> expression) =>
        await _entities.Where(expression).ToListAsync();

    public virtual async Task<Entity> GetByIdAsync(int entityId) =>
        await _entities.Where(entity => EF.Property<int>(entity, "Id").Equals(entityId)).SingleOrDefaultAsync();

    public virtual async Task InsertAsync(Entity entity) =>
        await _entities.AddAsync(entity);

    public virtual async Task AddRangeAsync(IEnumerable<Entity> entities) =>
        await _entities.AddRangeAsync(entities);

    public virtual void AttachRange(IEnumerable<Entity> entities) =>
        _entities.AttachRange(entities);

    /// <summary>
    /// Soft-delete by change value of status true -> false
    /// </summary>
    /// <param name="entity">Entity object</param>
    public virtual void Remove(Entity entity) =>
        entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);

    public virtual void Update(Entity entity) =>
        _entities.Update(entity);

    public virtual async Task<int> MaximumOrderIndexAsync(int personId)
    {
            var countElement = await _entities.Where(entity => EF.Property<int>(entity, "PersonId").Equals(personId)).CountAsync();

            // Find maximum with OrderIndex predicate
            return countElement == 0 ? 1 : countElement + 1;
        }

    public virtual async Task<IEnumerable<Entity>> GetAllAsync() =>
        await _entities.AsNoTracking().ToListAsync();

    /// <summary>
    /// Soft-delete by change value of status true -> false
    /// </summary>
    /// <param name="entities"></param>
    /// <returns>The number of entities deleted</returns>
    public virtual int RemoveRange(IEnumerable<Entity> entities)
    {
            int count = 0;
            foreach (var entity in entities)
            {
                entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
                count++;
            }

            return count;
        }

    public virtual async Task<IEnumerable<Entity>> GetWithPrimaryKeyAsync(List<int> keyValues)
    {
            var primaryKey = Context.Model.FindEntityType(typeof(Entity)).FindPrimaryKey();
            
            if (primaryKey.Properties.Count != 1)
                throw new NotSupportedException("Only a single primary key is supported");

            var pkPropertyName = primaryKey.Properties.First().Name;

            return await _entities.Where(entity => keyValues.Contains(EF.Property<int>(entity, pkPropertyName))).ToListAsync();
        }
    #endregion
}