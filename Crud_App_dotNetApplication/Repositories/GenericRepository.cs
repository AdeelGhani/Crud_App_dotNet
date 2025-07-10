using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Crud_App_dotNetApplication.Interfaces.IRepository;
using Crud_App_dotNetCore.Entities;
using Crud_App_dotNetInfrastructure.Data;

namespace Crud_App_dotNetApplication.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIdWithIncludesAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<IQueryable<T>, IQueryable<T>>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = include.Compile().Invoke(query);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludesAsync(Expression<Func<T, bool>> condition = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            // Apply the condition if provided
            if (condition != null)
            {
                query = query.Where(condition);
            }

            // Apply the includes
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }


        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate == null)
                return await _dbSet.CountAsync();

            return await _dbSet.CountAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }
        public async Task<List<T>> Where(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }



        public virtual async Task<T?> GetSingle(long key_code)
        {
            T? entity = await _dbSet
                .Where(rst => rst.Id == key_code)
                .FirstOrDefaultAsync();
            return entity;
        }

        public virtual async Task<T?> GetWithoutActiveStatus(long key_code)
        {
            T? entity = await _dbSet
                .Where(rst => rst.Id == key_code)
                .FirstOrDefaultAsync();
            return entity;
        }

        public long GetCount(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).Count();
        }

        public IQueryable<T> Search()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> SearchAsQueryable(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            IQueryable<T> query = _dbSet.Where(predicate);

            if (enableTracking)
                return query.AsTracking().AsQueryable();

            return query.AsNoTracking().AsQueryable();
        }

        public async Task<T?> SearchFirstOrDefault()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }

        public async Task<List<T>> SearchList()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual IEnumerable<T> GetAllList()
        {
            return _dbSet.AsEnumerable();
        }

        public async Task<T?> Search(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<List<T>> SearchList(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TResult?> Search<TResult>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> projection)
        {
            return await _dbSet.Where(predicate)
                .Select(projection)
                .FirstOrDefaultAsync();
        }

        public IQueryable<T> SearchWithIncludesAsQuerableNoTracking(Expression<Func<T, bool>> predicate,
            params Expression<Func<IQueryable<T>, IQueryable<T>>>[] includes)
        {
            IQueryable<T> queriedData = _context.Set<T>();

            foreach (Expression<Func<IQueryable<T>, IQueryable<T>>> include in includes)
            {
                queriedData = include.Compile().Invoke(queriedData);
            }

            return queriedData.Where(predicate).AsNoTracking();
        }



        public async Task<T?> SearchWithIncludes(Expression<Func<T, bool>> predicate,
            params Expression<Func<IQueryable<T>, IQueryable<T>>>[] includeExpressions)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includeExpressions)
            {
                query = include.Compile().Invoke(query);
            }

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T?> SearchWithIncludesNoTracking(Expression<Func<T, bool>> predicate,
            params Expression<Func<IQueryable<T>, IQueryable<T>>>[] includeExpressions)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includeExpressions)
            {
                query = include.Compile().Invoke(query);
            }

            return await query.Where(predicate).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<T>> SearchListWithIncludes(Expression<Func<T, bool>> predicate,
            params Expression<Func<IQueryable<T>, IQueryable<T>>>[] includeExpressions)
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeExpression in includeExpressions)
            {
                query = includeExpression.Compile().Invoke(query);
            }

            return await query.Where(predicate).ToListAsync();
        }

        public virtual async Task<List<TResult>> SearchList<TResult>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> projection)
        {
            return await _dbSet.Where(predicate)
                .Select(projection).ToListAsync();
        }

        public virtual async Task<List<TResult>> SearchList<TResult>(Expression<Func<T, bool>> predicate,
            Expression<Func<T, TResult>> projection, int fetchCount)
        {
            return await _dbSet.Where(predicate)
                .Select(projection).Take(fetchCount).ToListAsync();
        }

        public async Task<TResult?> Search<TResult>(Expression<Func<T, TResult>> projection)
        {
            return await _dbSet
                .Select(projection)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> SearchList<TResult>(Expression<Func<T, TResult>> projection)
        {
            return await _dbSet
                .Select(projection).ToListAsync();
        }

        public IEnumerable<T> GetAllList(long next_id)
        {
            var list = (from b in _dbSet
                        where b.Id > next_id
                        select b)
                .AsEnumerable();
            return list;
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
        public virtual async Task<List<T>> GetAll(long keycode)
        {
            return await _dbSet.Where(rst => rst.Id == keycode).ToListAsync();
        }

        public void Udpate(T entity)
        {
            /*_dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            // Ensure the Id property is not modified
            _context.Entry(entity).Property(c => c.Id).IsModified = false;*/

            var key = _context.Model?.FindEntityType(typeof(T))?.FindPrimaryKey()?
                      .Properties.FirstOrDefault()?.Name;

            if (key != null)
            {
                var entityKey = typeof(T)?.GetProperty(key)?.GetValue(entity);
                var existingEntity = _dbSet.Local
                    .FirstOrDefault(rst => typeof(T).GetProperty(key).GetValue(rst).Equals(entityKey));
                if (existingEntity != null)
                {
                    _context.Entry(existingEntity).State = EntityState.Detached;
                }
            }

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(prop => prop.Id).IsModified = false;
        }

        public bool ParentNavigationalUpdate(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(prop => prop.Id).IsModified = false;

            // Get all navigation properties (children)
            var navigationProperties = typeof(T).GetProperties()
                .Where(p => p.GetGetMethod() != null
                    && (typeof(IEnumerable<object>).IsAssignableFrom(p.PropertyType) || p.PropertyType.IsClass)
                    && !p.GetType().IsPrimitive && p.PropertyType != typeof(string));

            foreach (var navigationProperty in navigationProperties)
            {
                // Get the related child entities (e.g., collections of children)
                var children = navigationProperty.GetValue(entity);

                if (children == null)
                    continue;

                if (children is IEnumerable<object> childList)
                {
                    if (!childList.Any())
                        continue;

                    using (var enumerator = childList.GetEnumerator())
                    {
                        enumerator.MoveNext();
                        if (enumerator.Current.GetType().IsPrimitive || (enumerator.Current is string))
                            continue;
                    }

                    foreach (var child in childList)
                    {
                        var childEntry = _context.Entry(child);

                        if (childEntry.State == EntityState.Modified)
                        {
                            _context.Add(child);
                            _context.Entry(child).State = EntityState.Added;
                        }
                        else
                        {
                            // Attach the child entity and set its state to Modified
                            _context.Attach(child);
                            _context.Entry(child).State = EntityState.Modified;
                            _context.Entry(child).Property(nameof(BaseEntity.Id)).IsModified = false;
                        }


                    }
                }
                else
                {
                    var childEntry = _context.Entry(children);

                    if (childEntry.State != EntityState.Modified)
                    {
                        _context.Add(children);
                        _context.Entry(children).State = EntityState.Added;
                    }
                    else
                    {
                        // Attach the child entity and set its state to Modified
                        _context.Attach(children);
                        _context.Entry(children).State = EntityState.Modified;
                        _context.Entry(children).Property(nameof(BaseEntity.Id)).IsModified = false;
                    }
                }
            }

            return true;
        }

        public void Update_In_Range(List<T> list)
        {
            _context.UpdateRange(list);
        }

        public async Task Insert(T entity)
        {
            _context.Entry(entity).Property(prop => prop.Id).IsModified = false;
            await _dbSet.AddAsync(entity);
        }

        public async Task Insert_In_Range_Async(List<T> entity)
        {
            await _context.AddRangeAsync(entity);
        }

        public virtual async Task Delete(long keycode)
        {
            T? entity = await GetSingle(keycode);
            if (entity != null)
            {
                Delete(entity);
                //_dbSet.Remove(entity);
            }
            await _context.SaveChangesAsync();
        }

        public virtual void Delete(T entity)
        {
            /*_dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(prop => prop.Id).IsModified = false;
            _context.Entry(entity).Property(prop => prop.IsActiveRecord).IsModified = true;*/
            _dbSet.Remove(entity);
        }


        public bool CheckIfReferenced(long entityKeycode, Type entityType)
        {
            var entityModel = _context.Model.FindEntityType(entityType);
            if (entityModel == null)  // Corrected null check on entityModel
                return false;

            // Get all the foreign keys where the principal (referenced) entity is the given entity
            var foreignKeys = _context.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => fk.PrincipalEntityType == entityModel)
                .ToList();

            foreach (var foreignKey in foreignKeys)
            {
                var relatedEntityType = foreignKey.DeclaringEntityType.ClrType;

                // Use reflection to get the generic Set<T> method
                var method = typeof(DbContext).GetMethod(nameof(DbContext.Set), new Type[] { })
                    .MakeGenericMethod(relatedEntityType);

                var relatedDbSet = method.Invoke(_context, null) as IQueryable;

                if (relatedDbSet != null)
                {
                    var parameter = Expression.Parameter(relatedEntityType, "e");
                    var property = Expression.Property(parameter, foreignKey.Properties.First().Name);

                    // Handle nullable comparison
                    var convertedValue = Expression.Convert(Expression.Constant(entityKeycode), property.Type);
                    var equalsExpression = Expression.Equal(property, convertedValue);
                    var lambda = Expression.Lambda(equalsExpression, parameter);

                    var anyMethod = typeof(Queryable).GetMethods()
                        .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
                        .MakeGenericMethod(relatedEntityType);

                    var query = Expression.Call(null, anyMethod, relatedDbSet.Expression, lambda);

                    // Execute the query
                    var isReferenced = (bool)relatedDbSet.Provider.Execute(query);

                    if (isReferenced)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // Detach Method: Detach an entity from the EF Core change tracker
        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        // Method to check if an entity is already tracked and return it
        public T GetTrackedEntity(long keycode)
        {
            return _dbSet.Local.FirstOrDefault(e => e.Id == keycode);
        }
    }
}
