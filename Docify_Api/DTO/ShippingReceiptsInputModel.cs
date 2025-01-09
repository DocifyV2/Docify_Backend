using System.ComponentModel.DataAnnotations;
using Docify_Api.DataAnnotation;
using PostDigitaliser.AiDataClient.Models;
using PostDigitaliser.Server.Models.Enums;

namespace Docify_Api.DTO
{
    public class ShippingReceiptsInputModel
    {
        public Guid? Id { get; set; }
        [MaxLength(50)]
        public string LocationCode { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        [IsValidDate]
        public DateTime TimeStamp { get; set; }
        [MaxLength(50)]
        public string ShipmentCode { get; set; }
        [Required]
        [IsValidFile("jpg,jpeg,png", ErrorMessage = "Unsupported file type, should be of type: jpg, jpeg or png")]
        public IFormFile ReceiptImage { get; set; }
        public string ShipmentDetails { get; set; }
        public ShipmentProviderEnum ServiceProvider { get; set; }
        public ShipmentStatusEnum Status { get; set; }
    }
}
