using ETicaretAPI.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        public async Task DeleteAsync(string path, string fileName)
            => await Task.Run(() => { File.Delete($"{path}\\{fileName}"); });

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
            => File.Exists($"{path}\\{fileName}");

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
            return datas;
        }
    }
}
