using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;

        public ReadRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();

        public IQueryable<TEntity> GetAll()
            => Table;

        public Task<TEntity> GetByIdAsync(string id)
            => Table.FirstOrDefaultAsync(x => x.Id.ToString() == id);

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter)
            => await Table.FirstOrDefaultAsync(filter);

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> filter)
            => Table.Where(filter);
    }
}
