using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public string NameSurname { get; set; }
    }
}
