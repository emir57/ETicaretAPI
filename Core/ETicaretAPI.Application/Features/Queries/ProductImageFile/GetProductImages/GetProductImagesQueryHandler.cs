using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, GetProductImagesQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetProductImagesQueryResponse> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            ETicaretAPI.Domain.Entities.Product product = await _productReadRepository.Table
                .Include(p => p.ImageProducts)
                .ThenInclude(i => i.ProductImageFile)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return new GetProductImagesQueryResponse
            {
                Images = product.ImageProducts.Select(p => new
                {
                    p.ProductImageFile.Id,
                    p.ProductImageFile.Path,
                    p.ProductImageFile.FileName
                })
            };
        }
    }
}
