using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile
{
    public class RemoveProductImageFileHandler : IRequestHandler<RemoveProductImageFileCommandRequest, RemoveProductImageFileCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductImageFileHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<RemoveProductImageFileCommandResponse> Handle(RemoveProductImageFileCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productReadRepository.Table
                .Include(p => p.ImageProducts)
                .ThenInclude(i => i.ProductImageFile)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            Domain.Entities.ProductImage productImage = product.ImageProducts.FirstOrDefault(p => p.ProductImageFileId == Guid.Parse(request.ImageId));
            product.ImageProducts.Remove(productImage);
            await _productWriteRepository.SaveAsync();
            return new RemoveProductImageFileCommandResponse();
        }
    }
}
