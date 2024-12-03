namespace PostDigitaliser.Server.Wrappers.Interface
{
    public interface IAzureStorageWrapper
    {
        Task UploadImageAsync(IFormFile image, CancellationToken ct);
        Task<bool> DeleteImageAsync(string imageName);
        Task<string> RetrieveImageUrlAsync(string imageName);
    }
}
