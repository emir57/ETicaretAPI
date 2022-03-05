using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;

        public WriteRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public Task<bool> AddAsync(TEntity model)
        {
            
        }

        public Task<bool> AddRangeAsync(List<TEntity> model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(TEntity model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(TEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
