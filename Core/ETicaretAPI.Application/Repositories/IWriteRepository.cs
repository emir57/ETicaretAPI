﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T>
        where T:class
    {
    }
}