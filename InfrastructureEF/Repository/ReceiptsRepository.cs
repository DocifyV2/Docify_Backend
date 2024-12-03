using InfrastructureEF.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PostDigitaliser.Server.Models;
using PostDigitaliser.Server.Repository.Interfaces;

namespace PostDigitaliser.Server.Repository
{
    public class ReceiptsRepository(AppDbContext context, ILogger<ReceiptsRepository> logger)
        : IReceiptsRepository
    {
        public async Task<IEnumerable<Receipts>> GetReceipts()
        {
            return await context.Receipts
                .Include(x => x.ShipmentDetails)
                .Include(x => x.Image)
                .ToListAsync();
        }

        public async Task<Receipts?> GetReceipt(Guid id)
        {
            return await context.Receipts
                .Include(x => x.ShipmentDetails)
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}