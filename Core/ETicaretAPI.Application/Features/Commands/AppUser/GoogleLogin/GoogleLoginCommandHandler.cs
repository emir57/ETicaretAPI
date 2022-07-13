using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.Dtos;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IConfiguration _configuration;

        public GoogleLoginCommandHandler(UserManager<Domain.Identity.AppUser> userManager, ITokenHandler tokenHandler, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _configuration = configuration;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration.GetSection("GoogleAuthenticationKey").Value.ToString() }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
            var appUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            bool result = appUser != null;
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(payload.Email);
                if (appUser == null)
                {
                    appUser = new Domain.Identity.AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        NameSurname = payload.Name
                    };
                    var identityResult = await _userManager.CreateAsync(appUser);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
                await _userManager.AddLoginAsync(appUser, info);
            else
                throw new Exception("Invalid external authentication.");
            Token token = _tokenHandler.CreateAccessToken();
            return new GoogleLoginCommandResponse
            {
                Token = token
            };
        }
    }
}
