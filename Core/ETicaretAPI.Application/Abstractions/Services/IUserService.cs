using ETicaretAPI.Application.Dtos.User;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser createUser);
    }
}
