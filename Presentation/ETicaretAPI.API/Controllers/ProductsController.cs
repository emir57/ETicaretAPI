using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;

        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
        }
        [HttpGet("get")]
        public async Task Get()
        {
            /*
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
            */
            await _productWriteRepository.AddAsync(new Product
            {
                Name = "Product 2",
                Price = 22,
                Stock = 1,
                Id = Guid.NewGuid()
            });
            await _productWriteRepository.SaveAsync();
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] Pagination pagination)
        {
            await Task.Delay(1000);
            var totalCount = _productReadRepository.GetAll(tracking: false).Count();
            var products = _productReadRepository.GetAll(tracking: false).Select(x => new
            {
                x.Id,
                x.Name,
                x.Stock,
                x.Price,
                x.CreatedDate,
                x.UpdatedDate
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size).ToList();
            return Ok(new
            {
                totalCount,
                products
            });
        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id, tracking: false);
            return Ok(product);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            //TODO: refactoring
            await _productWriteRepository.AddAsync(new Product
            {
                Name = model.Name,
                Stock = model.Stock,
                Price = model.Price
            });
            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok(
                new
                {
                    message = "Silme işlemi başarılı."
                });
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            await _fileService.UploadAsync("resources/product-images", Request.Form.Files);
            return Ok();
        }
    }
}
