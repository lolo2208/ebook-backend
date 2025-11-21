using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookBackend.Infraestructure.FileStorage
{
    public class AzureBlobStorageService
    {
        private readonly BlobServiceClient _blobService;

        public AzureBlobStorageService(string connectionString)
        {
            _blobService = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadAsync(string container, string fileName, Stream fileStream)
        {
            var containerClient = _blobService.GetBlobContainerClient(container);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);

            return blobClient.Uri.ToString();
        }

        public async Task<bool> DeleteAsync(string container, string fileName)
        {
            var containerClient = _blobService.GetBlobContainerClient(container);
            var blobClient = containerClient.GetBlobClient(fileName);
            return await blobClient.DeleteIfExistsAsync();
        }
    }
}
