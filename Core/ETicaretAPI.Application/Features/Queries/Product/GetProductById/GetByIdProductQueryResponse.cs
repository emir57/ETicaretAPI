using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductById
{
    public class GetByIdProductQueryResponse
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
    }
}
