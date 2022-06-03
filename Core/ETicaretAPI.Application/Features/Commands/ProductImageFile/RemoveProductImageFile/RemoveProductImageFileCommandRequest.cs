using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile
{
    public class RemoveProductImageFileCommandRequest : IRequest<RemoveProductImageFileCommandResponse>
    {
        public string Id { get; set; }
        public string ImageId { get; set; }
    }
}
