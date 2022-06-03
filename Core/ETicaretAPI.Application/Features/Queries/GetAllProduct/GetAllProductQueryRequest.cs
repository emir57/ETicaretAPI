using ETicaretAPI.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryRequest
    {
        public Pagination Pagination { get; set; }
    }
}
