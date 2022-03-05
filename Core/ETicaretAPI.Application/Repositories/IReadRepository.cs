using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ETicaretAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T>
        where T:class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T,bool>> filter);
        T GetSingle(Expression<Func<T, bool>> filter);
        T GetById(string id);
    }
}
