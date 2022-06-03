using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImagesQueryRequest : IRequest<GetProductImagesQueryResponse>
    {
        public string Id { get; set; }
    }
}
