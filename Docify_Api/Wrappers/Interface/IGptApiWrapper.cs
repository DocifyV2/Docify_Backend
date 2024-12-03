using PostDigitaliser.Server.DTO;

namespace PostDigitaliser.Server.Wrappers.Interface
{
    public interface IGptApiWrapper
    {
        Task<IEnumerable<ReceiptsInputModel>> GetJsonFromImage();
        Task<bool> UploadImageAsync(IFormFile imageFile);
    }
}
