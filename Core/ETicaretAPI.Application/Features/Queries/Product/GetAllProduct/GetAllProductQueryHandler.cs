using ETicaretAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly ILogger _logger;

        public GetAllProductQueryHandler(IProductReadRepository productReadRepository, ILogger logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
        }
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get All Products");

            var totalCount = _productReadRepository.GetAll(tracking: false).Count();
            var products = await _productReadRepository.GetAll(tracking: false).Select(x => new
            {
                x.Id,
                x.Name,
                x.Stock,
                x.Price,
                x.CreatedDate,
                x.UpdatedDate
            }).Skip(request.Page * request.Size).Take(request.Size).ToListAsync();
            return new GetAllProductQueryResponse
            {
                TotalCount = totalCount,
                Products = products
            };
        }
    }
}
