using ETicaretAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryRequest : IRequest
    {
        public Pagination Pagination { get; set; }
    }
}
