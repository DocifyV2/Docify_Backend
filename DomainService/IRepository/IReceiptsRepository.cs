using PostDigitaliser.Server.Models;

namespace PostDigitaliser.Server.Repository.Interfaces
{
    public interface IReceiptsRepository
    {
        Task<IEnumerable<Receipts>> GetReceipts();
        Task<Receipts?> GetReceipt(Guid id);
    }
}
