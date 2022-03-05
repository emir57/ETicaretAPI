using ETicaretAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ProductWriteRepository : IProductWriteRepository
    {
        public DbSet<Domain.Entities.Product> Table => throw new System.NotImplementedException();

        public Task<bool> AddAsync(Domain.Entities.Product model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddRangeAsync(List<Domain.Entities.Product> models)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Domain.Entities.Product model)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveRange(List<Domain.Entities.Product> models)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Domain.Entities.Product model)
        {
            throw new System.NotImplementedException();
        }
    }
}
