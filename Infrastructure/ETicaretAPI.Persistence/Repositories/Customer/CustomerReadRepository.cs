using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.Customer
{
    public class CustomerReadRepository : ICustomerReadRepository
    {
        public DbSet<Customer> Table => throw new NotImplementedException();

        public IQueryable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetSingleAsync(Expression<Func<Customer, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Customer> GetWhere(Expression<Func<Customer, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
