using Domain.Models;
using PostDigitaliser.Server.Models.Enums;

namespace PostDigitaliser.Server.Models
{
    public class Receipts
    {
        public Guid? Id { get; set; }
        public Guid ImageId { get; set; } // Required foreign key property because not nullable
        public string LocationCode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string ShipmentCode { get; set; }
        public int Quantity { get; set; }
        public ShipmentProviderEnum ServiceProvider { get; set; }
        public ShipmentStatusEnum Status { get; set; }

        //Navigation properties

        //Receipts should always have an image, but is child since images can be of other types (like invoices in future)
        public Images Image { get; set; } = null!; // Required reference navigation to principal (images is parent here)
        public IEnumerable<ShipmentDetails> ShipmentDetails { get; set; }

    }
}
