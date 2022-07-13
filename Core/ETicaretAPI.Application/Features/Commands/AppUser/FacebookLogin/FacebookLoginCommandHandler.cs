using ETicaretAPI.Application.Abstractions.Token;
using Google.Apis.Http;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
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

            return new FacebookLoginCommandResponse
            {

            };
        }
    }
}
