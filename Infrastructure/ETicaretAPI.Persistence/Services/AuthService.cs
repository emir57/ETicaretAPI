using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class AuthService
    {
        private UserManager<AppUser> _userManager;

        public AuthService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        
    }
}
