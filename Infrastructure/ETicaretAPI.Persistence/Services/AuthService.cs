using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.Dtos;
using ETicaretAPI.Application.Dtos.Facebook;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Domain.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<AppUser> _userManager;
        private HttpClient _httpClient;
        private IConfiguration _configuration;
        private ITokenHandler _tokenHandler;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _httpClient = new HttpClient();
            _configuration = configuration;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
        }

        private async Task<Token> createUserExternalAsync(AppUser appUser, UserLoginInfo info, string email, string name, int accessTokenLifeTime = 15)
        {
            bool result = appUser != null;
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(email);
                if (appUser == null)
                {
                    appUser = new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        NameSurname = name
                    };
                    var identityResult = await _userManager.CreateAsync(appUser);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(appUser, info);
                Token token = _tokenHandler.CreateAccessToken();
                return token;
            }
            throw new Exception("Invalid external authentication.");
        }

        public async Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
        {
            string clientId = _configuration.GetSection("FacebookAuthenticationKey").Value;
            string appSecret = _configuration.GetSection("FacebookAppSecret").Value;
            string accessTokenResponse = await _httpClient.GetStringAsync(@$"https://graph.facebook.com/oauth/access_token
            ? client_id ={clientId}
            &client_secret ={appSecret}
            &grant_type = client_credentials");

            var accessTokenResponseDto = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);
            string userAccessTokenValidation = await _httpClient.GetStringAsync(@$"https://graph.facebook.com/oauth/access_token/debug_token
              ?input_token={authToken}&
              access_token={accessTokenResponseDto.AccessToken}");
            var validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

            if (validation.Data.IsValid)
            {
                string userInfoResponse = await _httpClient.GetStringAsync(@$"https://graph.facebook.com/ne?
                    fields=email,name&access_token={authToken}");
                var userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                var appUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                return await createUserExternalAsync(appUser, info, userInfo.Email, userInfo.Name);
            }
            throw new Exception("Invalid external authentication.");
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
        {
            string provider = "GOOGLE";
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration.GetSection("GoogleAuthenticationKey").Value.ToString() }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            var info = new UserLoginInfo(provider, payload.Subject, provider);
            var appUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await createUserExternalAsync(appUser, info, payload.Email, payload.Name);
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
        {
            string errorMessage = "Kullanıcı adı veya şifre hatalı";
            var user = await _userManager.FindByNameAsync(usernameOrEmail);

            if (user == null)
                user = await _userManager.FindByEmailAsync(password);

            if (user == null)
            {
                throw new NotFoundUserException();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken();
                return token;
            }
            throw new Exception(errorMessage);
        }
    }
}
