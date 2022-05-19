using System;
using System.Collections.Generic;
using System.Text;
using F = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Repositories
{
    public interface IFileReadRepository : IReadRepository<F.File>
    {
    }
}
