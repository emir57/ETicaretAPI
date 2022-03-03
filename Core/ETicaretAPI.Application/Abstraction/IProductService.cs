using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Abstraction
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
