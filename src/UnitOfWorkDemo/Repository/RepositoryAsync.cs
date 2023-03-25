using Microsoft.EntityFrameworkCore;
using UnitOfWorkDemo.Entities;

namespace UnitOfWorkDemo.Repository
{
    public interface IRepositoryAsync<T, TId> where T : class, IEntity<TId>
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(TId id);

        Task<List<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }

    public class RepositoryAsync<T, TId> : IRepositoryAsync<T, TId> where T : BaseEntity<TId>
    {
        private readonly AppDbContext _dbContext;

        public RepositoryAsync(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            T existingItem = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(existingItem).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }
    }
}