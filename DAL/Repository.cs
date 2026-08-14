using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using team_management_system.DAL.Interface;

namespace team_management_system.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly teamManagementSystemDBContext _dbContext;
        private DbSet<T> _dbSet;

        public Repository(teamManagementSystemDBContext dbContext)
        { 
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetDetailsAsync(Expression<Func<T, bool>> condition, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await _dbSet.AsNoTracking().Where(condition).FirstOrDefaultAsync();
            } else
            {
                return await _dbSet.Where(condition).FirstOrDefaultAsync();
            }
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<T> GetDetailsByIdAsync(int id)
        {
            var details = await _dbSet.FindAsync(id);
            return details;
        }
    }
}
