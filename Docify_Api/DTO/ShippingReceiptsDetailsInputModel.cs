namespace Docify_Api.DTO
{
    public class ShippingReceiptsDetailsInputModel
    {
        //The actual track and trace code
        public Guid? Id { get; set; }
        public string BarCode { get; set; }
        public string PostalCode { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Quantity { get; set; }
    }
}
