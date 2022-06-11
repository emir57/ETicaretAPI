using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            CreateUserCommandResponse response =
                new CreateUserCommandResponse { Succeeded = result.Succeeded };
            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur";
            else
            {
                response.Message = String.Join("\n", result.Errors.Select(e => $"{e.Code} - {e.Description}"));
            }
            return response;
            //throw new UserCreateFailedException();
        }
    }
}
