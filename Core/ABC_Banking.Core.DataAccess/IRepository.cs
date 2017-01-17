using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ABC_Banking.Core.DataAccess
{
    internal interface IRepository<T>
    {
        Task<T> GetById(object id);
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            string includeProperties);
        void Insert(T entity);
        void Update(T entity);
    }
}
