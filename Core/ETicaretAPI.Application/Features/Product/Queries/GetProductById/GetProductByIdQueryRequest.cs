using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Features.Product.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
    {
    }
}
