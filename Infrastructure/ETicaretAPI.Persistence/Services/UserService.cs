using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
    }
}
