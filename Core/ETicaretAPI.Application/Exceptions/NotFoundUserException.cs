﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException() : base("Kullanıcı adı veya şifer hatalı")
        {
        }

        public NotFoundUserException(string message) : base(message)
        {
        }

        public NotFoundUserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
