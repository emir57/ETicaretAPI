using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        void CreateAccessToken();
    }
}
