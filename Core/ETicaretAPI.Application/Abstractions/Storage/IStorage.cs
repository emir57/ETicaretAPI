using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainer)>> UploadAsync
            (string pathOrContainer, IFormFileCollection formFiles);
        Task DeleteAsync(string pathOrContainer, string fileName);
    }
}
