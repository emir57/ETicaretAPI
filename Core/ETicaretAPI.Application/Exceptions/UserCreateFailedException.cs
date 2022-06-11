﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ETicaretAPI.Application.Exceptions
{
    public class UserCreateFailedException : Exception
    {
        public UserCreateFailedException()
        {

        }

        public UserCreateFailedException(string message) : base(message)
        {
        }

        public UserCreateFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
