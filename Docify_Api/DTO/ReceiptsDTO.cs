using PostDigitaliser.AiDataClient.Models;
using PostDigitaliser.Server.Models.Enums;

namespace PostDigitaliser.Server.DTO
{
    public class ReceiptsDTO
    {
        public Guid? Id { get; set; }
        public string LocationCode { get; set; }
        public string Address { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ShipmentCode { get; set; }
        public IEnumerable<ReceiptsDetailsDTO> ShipmentDetails { get; set; }
        public ShipmentProviderEnum ServiceProvider { get; set; }
        public ShipmentStatusEnum Status { get; set; }
    }
}
