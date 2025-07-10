using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Crud_App_dotNetCore.Entities;
//using NPOI.SS.Formula.Functions;

namespace Crud_App_dotNetApplication.Interfaces.IRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdWithIncludesAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<IQueryable<T>, IQueryable<T>>>[] includes);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> Query();
        Task<List<T>> Where(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllWithIncludesAsync(Expression<Func<T, bool>> condition = null, params Expression<Func<T, object>>[] includes);

        Task<List<T>> SearchListWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<IQueryable<T>, IQueryable<T>>>[] includes);

        Task<T?> SearchWithIncludes(Expression<Func<T, bool>> predicate, params Expression<Func<IQueryable<T>, IQueryable<T>>>[] includeExpressions);
    }
}
