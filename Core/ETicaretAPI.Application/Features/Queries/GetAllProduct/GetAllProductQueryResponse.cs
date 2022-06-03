using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Features.Queries.GetAllProduct
{
    public class GetAllProductQueryResponse
    {
        public int TotalCount { get; set; }
        public List<Product> Products { get; set; }
    }
}
