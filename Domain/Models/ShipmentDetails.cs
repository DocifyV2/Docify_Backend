namespace PostDigitaliser.Server.Models
{
    public class ShipmentDetails
    {
        public Guid? Id { get; set; }
        public Guid ReceiptId { get; set; }
        public string BarCode { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        //Navigation properties
        public Receipts Receipts { get; set; }
    }
}
