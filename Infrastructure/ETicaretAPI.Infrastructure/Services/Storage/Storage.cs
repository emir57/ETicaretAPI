using ETicaretAPI.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected async Task<string> FileRenameAsync(string path, string fileName, int num = 1)
        {
            return await Task.Run(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldName = $"{Path.GetFileNameWithoutExtension(fileName)}-{num}";
                string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";

                if (File.Exists($"{path}\\{newFileName}"))
                {
                    return await FileRenameAsync(path, $"{newFileName.Split($"-{num}")[0]}{extension}", ++num);
                }
                return newFileName;
            });
        }
    }
}
