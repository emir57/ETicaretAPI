using ETicaretAPI.Application.Abstraction;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
            => new List<Product>() 
            {
                new Product{Id=Guid.NewGuid(),Name="Product 1",Price=100,Stock=10,CreatedDate=DateTime.Now},
                new Product{Id=Guid.NewGuid(),Name="Product 2",Price=1000,Stock=15,CreatedDate=DateTime.Now},
                new Product{Id=Guid.NewGuid(),Name="Product 3",Price=17,Stock=22,CreatedDate=DateTime.Now},
                new Product{Id=Guid.NewGuid(),Name="Product 4",Price=10,Stock=30,CreatedDate=DateTime.Now},
                new Product{Id=Guid.NewGuid(),Name="Product 5",Price=50,Stock=41,CreatedDate=DateTime.Now},
                new Product{Id=Guid.NewGuid(),Name="Product 6",Price=230,Stock=5,CreatedDate=DateTime.Now},
            };
    }
}
