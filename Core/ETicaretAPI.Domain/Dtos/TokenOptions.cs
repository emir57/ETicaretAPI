using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Domain.Dtos
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
        public int AccessTokenExpiration { get; set; }
    }
}
