﻿using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.Dtos;
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
        private readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(SignInManager<Domain.Identity.AppUser> signInManager, UserManager<Domain.Identity.AppUser> userManager, ITokenHandler tokenHandler)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
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
                return new LoginUserErrorCommandResponse()
                    .AddMessage(errorMessage)
                    .AddSucceeded(false);
                //throw new NotFoundUserException();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                return new LoginUserSuccessCommandResponse()
                    .AddToken(_tokenHandler.CreateAccessToken())
                    .AddSucceeded(true);
                //TODO: authorization
            }
            return new LoginUserErrorCommandResponse()
                    .AddMessage(errorMessage)
                    .AddSucceeded(false); ;
        }
    }
}
