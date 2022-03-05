using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<bool> AddAsync(TEntity model)
        {
            EntityEntry<TEntity> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<TEntity> models)
        {
            await Table.AddRangeAsync(models);
            return true;
        }

        public bool Remove(TEntity model)
        {
            EntityEntry<TEntity> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            TEntity entity = await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            EntityEntry<TEntity> entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<TEntity> models)
        {
            Table.RemoveRange(models);
            return true;
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public bool Update(TEntity model)
        {
            EntityEntry<TEntity> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
