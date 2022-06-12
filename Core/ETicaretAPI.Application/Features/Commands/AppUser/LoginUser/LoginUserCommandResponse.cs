using ETicaretAPI.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Token Token { get; set; }
    }
}
