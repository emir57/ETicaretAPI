using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formFiles);
        Task<string> FileRenameAsync(string fileName);
        Task<bool> CopyFileAsync(string path, IFormFile file);
    }
}
