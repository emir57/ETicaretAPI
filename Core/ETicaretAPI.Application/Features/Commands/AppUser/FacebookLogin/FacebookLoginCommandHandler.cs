using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.Dtos;
using ETicaretAPI.Application.Dtos.Facebook;
using Google.Apis.Http;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        private readonly UserManager<Domain.Identity.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public FacebookLoginCommandHandler(UserManager<Domain.Identity.AppUser> userManager, ITokenHandler tokenHandler, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateHttpClient(new CreateHttpClientArgs { });
            _configuration = configuration;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            string clientId = _configuration.GetSection("FacebookAuthenticationKey").Value;
            string appSecret = _configuration.GetSection("FacebookAppSecret").Value;
            string accessTokenResponse = await _httpClient.GetStringAsync(@$"https://graph.facebook.com/oauth/access_token
            ? client_id ={clientId}
            &client_secret ={appSecret}
            &grant_type = client_credentials");

            var accessTokenResponseDto = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);
            string userAccessTokenValidation = await _httpClient.GetStringAsync(@$"https://graph.facebook.com/oauth/access_token/debug_token
              ?input_token={request.AuthToken}&
              access_token={accessTokenResponseDto.AccessToken}");

            var validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);
            if (validation.Data.IsValid)
            {
                string userInfoResponse = await _httpClient.GetStringAsync(@$"https://graph.facebook.com/ne?
                    fields=email,name&access_token={request.AuthToken}");
                var userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                var appUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                bool result = appUser != null;
                if (appUser == null)
                {
                    appUser = await _userManager.FindByEmailAsync(userInfo.Email);
                    if (appUser == null)
                    {
                        appUser = new Domain.Identity.AppUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = userInfo.Email,
                            UserName = userInfo.Email,
                            NameSurname = userInfo.Name
                        };
                        var identityResult = await _userManager.CreateAsync(appUser);
                        result = identityResult.Succeeded;
                    }
                }
                if (result)
                {
                    await _userManager.AddLoginAsync(appUser, info);
                    Token token = _tokenHandler.CreateAccessToken();
                    return new FacebookLoginCommandResponse
                    {
                        Token = token
                    };
                }
            }
            throw new Exception("Invalid external authentication.");
        }
    }
}
