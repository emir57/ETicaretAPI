using Microsoft.AspNetCore.Identity;
using System;

namespace ETicaretAPI.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public string NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenDate { get; set; }
    }
}
