using PostDigitaliser.AiDataClient.Models;
using PostDigitaliser.Server.Models.Enums;

namespace PostDigitaliser.Server.DTO
{
    public class ReceiptsInputModel
    {
        public Guid? Id { get; set; }
        public string LocationCode { get; set; }
        public string Address { get; set; }
        public DateTime Timestamp { get; set; }
        public string ShipmentCode { get; set; }
        public IEnumerable<ShipmentDetailsInputModel> ShipmentDetails { get; set; }
        public ShipmentProviderEnum ServiceProvider { get; set; }
        public ShipmentStatusEnum Status { get; set; }
    }

    public class TimeStamp
    {
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
