using Azure.Storage.Blobs;
using ETicaretAPI.Application.Abstractions.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["Storage:Azure"]);
        }

        public Task DeleteAsync(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles(string pathOrContainerName)
        {
            throw new NotImplementedException();
        }

        public bool HasFile(string pathOrContainerName, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection formFiles)
        {
            throw new NotImplementedException();
        }
    }
}
