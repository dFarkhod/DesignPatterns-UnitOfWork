using Microsoft.EntityFrameworkCore;
using UnitOfWorkDemo.DTOs;
using UnitOfWorkDemo.Entities;

namespace UnitOfWorkDemo.Repository
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> Entities { get; }

        Task<Department> GetByIdAsync(int id);

        Task<List<Department>> GetAllAsync();

        Task<Department> AddAsync(Department entity);

        Task UpdateAsync(Department entity);

        Task DeleteAsync(Department entity);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;
        public IQueryable<Department> Entities => _dbContext.Set<Department>();

        public DepartmentRepository()
        {
        }

        public async Task<Department> AddAsync(Department entity)
        {
            await _dbContext.Set<Department>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Department entity)
        {
            _dbContext.Set<Department>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _dbContext.Set<Department>().ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Department>().FindAsync(id);
        }

        public async Task UpdateAsync(Department entity)
        {
            Department existingItem = _dbContext.Set<Department>().Find(entity.Id);
            _dbContext.Entry(existingItem).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}