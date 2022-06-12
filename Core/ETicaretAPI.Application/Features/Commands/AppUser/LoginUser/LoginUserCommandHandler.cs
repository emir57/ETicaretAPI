using ETicaretAPI.Application.Exceptions;
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

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            string errorMessage = "Kullanıcı adı veya şifre hatalı";
            string successMessage = "Giriş başarılı";
            var user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            var response = new LoginUserCommandResponse();
            if (user == null)
            {
                response.Succeeded = false;
                response.Message = errorMessage;
                //throw new NotFoundUserException();
                return response;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            response.Succeeded = result.Succeeded;
            response.Message = errorMessage;
            if (result.Succeeded)
            {
                response.Message = successMessage;
                //TODO: authorization
            }
            return response;
        }
    }
}
