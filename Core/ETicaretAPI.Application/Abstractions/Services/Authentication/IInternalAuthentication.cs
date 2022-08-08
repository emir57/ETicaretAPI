using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task<Dtos.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
        Task<Dtos.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
