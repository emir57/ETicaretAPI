using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImageFile
{
    public class UploadProductImageFileCommandHandler : IRequestHandler<UploadProductImageFileCommandRequest, UploadProductImageFileCommandResponse>
    {
        public Task<UploadProductImageFileCommandResponse> Handle(UploadProductImageFileCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
