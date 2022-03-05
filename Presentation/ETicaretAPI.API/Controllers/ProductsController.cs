using ETicaretAPI.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        [HttpGet("get")]
        public async Task Get()
        {
            await _productWriteRepository.AddRangeAsync(new List<Domain.Entities.Product>
            {
                new Domain.Entities.Product{
                    Id = Guid.NewGuid(),CreatedDate = DateTime.Now,
                    Name = "Product 1",
                    Stock = 5,
                    Price = (long)299.9},
                new Domain.Entities.Product
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.AddDays(1),
                    Name = "Product 2",
                    Stock = 2,
                    Price = (long)929.9},
                new Domain.Entities.Product
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now.AddDays(2),
                    Name = "Product 3",
                    Stock = 7,
                    Price = (long)102.9}
            });
            await _productWriteRepository.SaveAsync();
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            return Ok(_productReadRepository.GetAll());
        }
    }
}
