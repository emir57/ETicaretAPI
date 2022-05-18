using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Services
{
    public interface IFileService
    {
        Task UploadAsync(string path, IFormFileCollection formFiles);
        Task<string> FileRenameAsync(string fileName);
    }
}
