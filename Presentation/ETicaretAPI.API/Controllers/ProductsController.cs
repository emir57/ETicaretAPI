using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetProductById;
using ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            var getAllProductQueryResponse = await _mediator.Send(getAllProductQueryRequest);
            return Ok(getAllProductQueryResponse);

        }
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromQuery] GetByIdProductQueryRequest getProductByIdQueryRequest)
        {
            var getProductByIdQueryResponse = await _mediator.Send(getProductByIdQueryRequest);
            return Ok(getProductByIdQueryResponse);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            var createProductResponse = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(UpdateProductCommandRequest updateProductCommandRequest)
        {
            var updateProductCommandResponse = await _mediator.Send(updateProductCommandRequest);
            return Ok(updateProductCommandResponse);
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(RemoveProductCommandRequest removeProductCommandRequest)
        {
            var removeProductCommandResponse = await _mediator.Send(removeProductCommandRequest);
            return Ok(removeProductCommandResponse);
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
        public async Task<IActionResult> GetProductImages(GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            Thread.Sleep(500);
            var getProductImagesQueryResponse = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(getProductImagesQueryResponse);
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
