using ETicaretAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Local
{
    public class LocalStorage : ILocalStorage
    {
        public Task DeleteAsync(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string path)
        {
            throw new NotImplementedException();
        }

        public bool HasFile(string path, string fileName)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
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
                catch (Exception e)
                {
                    //TODO: log
                    throw e;
                }
            }
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection formFiles)
        {
            string uploadPath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",
                path);
            if (!File.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas =
                new List<(string fileName, string path)>();
            foreach (IFormFile file in formFiles)
            {
                await CopyFileAsync(Path.Combine(uploadPath, file.Name), file);
                datas.Add((file.Name, Path.Combine(path, file.Name)));
            }
            return null;
        }
    }
}
