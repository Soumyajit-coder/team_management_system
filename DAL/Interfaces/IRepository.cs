using System.Linq.Expressions;

namespace team_management_system.DAL.Interface
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetDetailsAsync(Expression<Func<T, bool>> condition, bool useNoTracking = false);
        Task<T> CreateAsync(T entity);
        Task<T> GetDetailsByIdAsync(int id);
        Task<int> GetLastIdAsync(Expression<Func<T, int>> idSelector);
    }
}
