using ETicaretAPI.Application.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            await using (FileStream fileStream = new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                1024 * 1024,
                useAsync: false))
            {
                try
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public Task<string> FileRenameAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task UploadAsync(string path, IFormFileCollection formFiles)
        {
            string uploadPath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",
                path);
            if (!File.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (IFormFile file in formFiles)
            {
                string fileNewName = await FileRenameAsync(file.FileName);
                bool isUpload = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);
            }
        }
    }
}
