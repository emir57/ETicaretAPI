using ETicaretAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.Product
{
    public class ProductReadRepository : IProductReadRepository
    {
        public DbSet<Domain.Entities.Product> Table => throw new NotImplementedException();

        public IQueryable<Domain.Entities.Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Product> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Product> GetSingleAsync(Expression<Func<Domain.Entities.Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Domain.Entities.Product> GetWhere(Expression<Func<Domain.Entities.Product, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
