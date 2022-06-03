using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImageFile
{
    public class RemoveProductImageFileHandler : IRequestHandler<RemoveProductImageFileCommandRequest, RemoveProductImageFileCommandResponse>
    {
        public Task<RemoveProductImageFileCommandResponse> Handle(RemoveProductImageFileCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
