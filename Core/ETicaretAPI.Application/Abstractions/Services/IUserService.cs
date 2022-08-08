using ETicaretAPI.Application.Dtos.User;
using ETicaretAPI.Domain.Identity;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser createUser);
        Task UpdateRefreshToken(AppUser appUser, string refreshToken);
    }
}
