using ETicaretAPI.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        public bool Succeeded { get; set; }

        public LoginUserCommandResponse AddSucceeded(bool succeeded)
        {
            Succeeded = succeeded;
            return this;
        }
    }
    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public Token Token { get; set; }

        public LoginUserSuccessCommandResponse AddToken(Token token)
        {
            Token = token;
            return this;
        }
    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }

        public LoginUserErrorCommandResponse AddMessage(string message)
        {
            Message = message;
            return this;
        }
    }
}
