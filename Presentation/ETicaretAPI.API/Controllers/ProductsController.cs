using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Features.Commands.CreateProduct;
using ETicaretAPI.Application.Features.Queries.GetAllProduct;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.Services;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
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

        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IStorageService _storageService;

        private readonly IMediator _mediator;
        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService, IMediator mediator)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;

            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
            _mediator = mediator;
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
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            var getAllProductQueryResponse = await _mediator.Send(getAllProductQueryRequest);
            return Ok(getAllProductQueryResponse);

        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id, tracking: false);
            return Ok(product);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            var createProductResponse = await _mediator.Send(createProductCommandRequest);
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
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

            await _productImageFileWriteRepository.AddRangeAsync(
                result.Select(r => new ProductImageFile
                {
                    FileName = r.fileName,
                    Path = r.pathOrContainerName,
                    Storage = _storageService.StorageName,
                    ImageProducts = new List<ProductImage> { new ProductImage{
                        Product = product,
                        ProductImageFile = new ProductImageFile
                                        {FileName=r.fileName,Path=r.pathOrContainerName,Storage="Local"}
                    } }
                }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpGet("productimages")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Thread.Sleep(500);
            Product product = await _productReadRepository.Table
                .Include(p => p.ImageProducts)
                .ThenInclude(i => i.ProductImageFile)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            return Ok(product.ImageProducts.Select(p => new
            {
                p.ProductImageFile.Id,
                p.ProductImageFile.Path,
                p.ProductImageFile.FileName
            }));
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            Thread.Sleep(300);
            Product product = await _productReadRepository.Table
                .Include(p => p.ImageProducts)
                .ThenInclude(i => i.ProductImageFile)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            ProductImage productImage = product.ImageProducts.FirstOrDefault(p => p.ProductImageFileId == Guid.Parse(imageId));
            product.ImageProducts.Remove(productImage);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }
    }
}
