﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ETicaretAPI.Application.Abstractions.Storage
{
    public interface IStorageService : IStorage
    {
        string StorageName { get;}
    }
}
