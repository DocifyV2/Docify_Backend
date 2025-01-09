using PostDigitaliser.Server.DTO;

namespace Docify_Api.Services.Interfaces
{
    public interface IReceiptsService
    {
        Task<List<ReceiptsDTO>> GetReceipts();
        Task<ReceiptsDTO> GetReceipt(Guid id);
    }
}
