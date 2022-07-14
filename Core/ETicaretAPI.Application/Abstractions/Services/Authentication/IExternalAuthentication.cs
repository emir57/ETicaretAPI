using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task<Dtos.Token> FacebookLoginAsync(string authToken);
        Task<Dtos.Token> GoogleLoginAsync(string idToken);

        //Task TwitterLoginAsync();
    }
}
