using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
