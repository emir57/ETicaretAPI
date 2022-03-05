using ETicaretAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class OrderWriteRepository : IOrderWriteRepository
    {
        public DbSet<Domain.Entities.Order> Table => throw new NotImplementedException();

        public Task<bool> AddAsync(Domain.Entities.Order model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddRangeAsync(List<Domain.Entities.Order> models)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Domain.Entities.Order model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRange(List<Domain.Entities.Order> models)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public bool Update(Domain.Entities.Order model)
        {
            throw new NotImplementedException();
        }
    }
}
