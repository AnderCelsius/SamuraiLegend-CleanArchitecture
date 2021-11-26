using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiLegend.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
        );
        Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task<T> GetByIdAsync(string Id);
        Task<T> GetByIdAsync(int Id);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task AddAsync(T entity);        
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        void UpdateAsync(T entity);
    }
}
