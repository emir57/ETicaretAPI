using ETicaretAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.Customer
{
    public class CustomerWriteRepository : ICustomerWriteRepository
    {
        public DbSet<Domain.Entities.Customer> Table => throw new System.NotImplementedException();

        public Task<bool> AddAsync(Domain.Entities.Customer model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddRangeAsync(List<Domain.Entities.Customer> models)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Domain.Entities.Customer model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveRange(List<Domain.Entities.Customer> models)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Domain.Entities.Customer model)
        {
            throw new System.NotImplementedException();
        }
    }
}
