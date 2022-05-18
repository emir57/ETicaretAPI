using ETicaretAPI.Application.Services;
using ETicaretAPI.Infrastructure.Operations;
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
                catch (Exception e)
                {
                    //TODO: log
                    throw e;
                }
            }
        }

        private Task<string> FileRenameAsync(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
            
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
                string fileNewName = await FileRenameAsync(file.FileName);
                bool result = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);
                datas.Add((fileNewName, Path.Combine(uploadPath, fileNewName)));
                results.Add(result);
            }
            if (results.TrueForAll(r => r.Equals(true)))
                return datas;
            return null;

            //TODO: if the result is wrong create custom exception
        }
    }
}
