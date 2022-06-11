using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        public CreateUserCommandHandler(UserManager<Domain.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Identity.AppUser user = new Domain.Identity.AppUser
            {
                UserName = request.Username,
                Email = request.Email,
                NameSurname = request.FirstLastName
            };
            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
                return new CreateUserCommandResponse()
                {
                    Message = "Kullanıcı başarıyla oluşturulmuştur",
                    Succeeded = true
                };
            return new CreateUserCommandResponse
            {
                Succeeded = false,
                Message = ""
            };

        }
    }
}
