using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private Application.Dtos.TokenOptions _tokenOptions;

        public UserService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenOptions = configuration.GetSection("TokenOptions").Get<Application.Dtos.TokenOptions>();
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            AppUser user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.FirstLastName
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            CreateUserResponse response =
                new CreateUserResponse { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur";
            else
                response.Message = String.Join("\n", result.Errors.Select(e => $"{e.Code} - {e.Description}"));

            return response;
        }

        public async Task UpdateRefreshToken(AppUser appUser, string refreshToken)
        {
            if (appUser != null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenDate = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration + 15);
                await _userManager.UpdateAsync(appUser);
            }
            throw new NotFoundUserException();
        }
    }
}
