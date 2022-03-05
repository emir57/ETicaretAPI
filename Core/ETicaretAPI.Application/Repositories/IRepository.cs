using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Repositories
{
    public interface IRepository<T>
        where T:class
    {
        DbSet<T> Table { get; }
    }
}
