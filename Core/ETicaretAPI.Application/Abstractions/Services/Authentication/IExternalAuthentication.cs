using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task FacebookLoginAsync();
        Task GoogleLoginAsync();
        Task TwitterLoginAsync();
    }
}
