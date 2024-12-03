using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Identity.Client;
using PostDigitaliser.Server.Wrappers.Interface;

namespace PostDigitaliser.Server.Wrappers
{
    public class AzureStorageWrapper : IAzureStorageWrapper
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _blobContainerClient;

        public AzureStorageWrapper(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("shippingreceipts");
        }

        public async Task UploadImageAsync(IFormFile image, CancellationToken ct)
        {
            await using var stream = image.OpenReadStream();

            await _blobContainerClient.UploadBlobAsync(image.FileName, stream, ct);

            stream.Close();
        }

        public async Task<bool> DeleteImageAsync(string imageName)
        {
            return await _blobContainerClient.DeleteBlobIfExistsAsync(imageName);
        }

        public async Task<string> RetrieveImageUrlAsync(string imageName)
        {
            return _blobContainerClient.Uri.AbsoluteUri;
        }
    }
}
