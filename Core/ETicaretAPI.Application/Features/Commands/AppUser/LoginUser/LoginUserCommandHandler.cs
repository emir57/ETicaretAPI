using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        private readonly SignInManager<Domain.Identity.AppUser> _signInManager;

        public LoginUserCommandHandler(SignInManager<Domain.Identity.AppUser> signInManager, UserManager<Domain.Identity.AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
