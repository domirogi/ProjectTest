using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Project.Common;
using Project.DAL.Data;
using Project.Repository.Common.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected VehicleDbContext _context;
        private readonly DbSet<T> _db;
        public GenericRepository(VehicleDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _db;
            if (include != null)
            {
                query = include(query);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }


        public async Task<IPagedList<T>> GetPagedList(RequestParams requestParams, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _db;


            if (include != null)
            {
                query = include(query);
            }

            return await query.AsNoTracking()
                .ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        private IQueryable<T> GetByCondition(
         Expression<Func<T, bool>> filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties ??= string.Empty;
            IQueryable<T> query = _db;

            if (filter != null)
            {
                query = query.Where(filter);

            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public Task<List<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            return GetByCondition(null, orderBy, includeProperties).AsNoTracking().ToListAsync();
        }

        public Task CreateAsync(T entity)
        {
            _db.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            _db.Remove(entity);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }

       
    }
}
