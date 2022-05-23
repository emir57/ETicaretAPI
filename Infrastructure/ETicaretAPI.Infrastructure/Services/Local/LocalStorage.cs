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

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection formFiles)
        {
            string uploadPath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",
                path);
            if (!File.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas =
                new List<(string fileName, string path)>();
            List<bool> results = new List<bool>();
            foreach (IFormFile file in formFiles)
            {
                bool result = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);
                datas.Add((fileNewName, Path.Combine(path, fileNewName)));
                results.Add(result);
            }
            if (results.TrueForAll(r => r.Equals(true)))
                return datas;
            return null;
        }
    }
}
