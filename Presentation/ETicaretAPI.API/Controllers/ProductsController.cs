using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImageFile;
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
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
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
        public async Task<IActionResult> Upload([FromQuery,FromForm] UploadProductImageFileCommandRequest
            uploadProductImageFileCommandRequest)
        {
            var uploadProductImageFileResponse = await _mediator.Send(uploadProductImageFileCommandRequest);
            return Ok(uploadProductImageFileResponse);
        }
        [HttpGet("productimages")]
        public async Task<IActionResult> GetProductImages([FromQuery] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            var getProductImagesQueryResponse = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(getProductImagesQueryResponse);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProductImage([FromQuery] RemoveProductImageFileCommandRequest removeProductImageFileCommandRequest)
        {
            var removeProductImageFileCommandResponse = await _mediator.Send(removeProductImageFileCommandRequest);
            return Ok(removeProductImageFileCommandResponse);
        }
    }
}
