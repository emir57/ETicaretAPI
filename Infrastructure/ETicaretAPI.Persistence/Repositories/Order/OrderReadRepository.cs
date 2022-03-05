using ETicaretAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class OrderReadRepository : IOrderReadRepository
    {
        public DbSet<Domain.Entities.Order> Table => throw new NotImplementedException();

        public IQueryable<Domain.Entities.Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Order> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Order> GetSingleAsync(Expression<Func<Domain.Entities.Order, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Domain.Entities.Order> GetWhere(Expression<Func<Domain.Entities.Order, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
