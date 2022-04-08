using Microsoft.EntityFrameworkCore.Query;
using Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Project.Repository.Common.IRepository
{
  public  interface IGenericRepository<T> where T : class
    {

        Task<List<T>> GetAllAsync(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null);

        Task<IPagedList<T>> GetPagedList(
            RequestParams requestParams,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            );

        Task<T> Get(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

       
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
