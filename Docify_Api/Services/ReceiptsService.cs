using PostDigitaliser.AiDataClient.Models;
using PostDigitaliser.Server.DTO;
using PostDigitaliser.Server.Repository;
using System.Globalization;
using Docify_Api.Services.Interfaces;
using PostDigitaliser.Server.Repository.Interfaces;

namespace Docify_Api.Services
{
    public class ReceiptsService(IReceiptsRepository receiptsRepository) : IReceiptsService
    {
        public async Task<List<ReceiptsDTO>> GetReceipts()
        {
            // var result = await gptApiWrapper.GetJsonFromImage();
            var receipts = await receiptsRepository.GetReceipts();

            var result = new List<ReceiptsDTO>();

            foreach (var receipt in receipts)
            {
                var shipmentDetails = receipt.ShipmentDetails
                    .Select(shipmentDetail => new ReceiptsDetailsDTO
                    {
                        Id = shipmentDetail.Id,
                        BarCode = shipmentDetail.BarCode,
                        PostalCode = shipmentDetail.PostalCode,
                        HouseNumber = shipmentDetail.HouseNumber,
                        City = shipmentDetail.City,
                        Country = shipmentDetail.Country,
                    })
                    .ToList();

                result.Add(new ReceiptsDTO
                {
                    Id = receipt.Id,
                    LocationCode = receipt.LocationCode,
                    Address = $"{receipt.Street} {receipt.Number}",
                    TimeStamp = DateTime.ParseExact($"{receipt.Date} {receipt.Time}", "yyyy-MM-dd HH:mm", null,
                        DateTimeStyles.None),
                    ShipmentCode = receipt.ShipmentCode,
                    ShipmentDetails = shipmentDetails,
                    ServiceProvider = receipt.ServiceProvider,
                    Status = receipt.Status
                });
            }

            return result;
        }

        public async Task<ReceiptsDTO> GetReceipt(Guid id)
        {
            var receipt = await receiptsRepository.GetReceipt(id);

            var shipmentDetails = receipt.ShipmentDetails
                .Select(shipmentDetail => new ReceiptsDetailsDTO
                {
                    Id = shipmentDetail.Id,
                    BarCode = shipmentDetail.BarCode,
                    PostalCode = shipmentDetail.PostalCode,
                    HouseNumber = shipmentDetail.HouseNumber,
                    City = shipmentDetail.City,
                    Country = shipmentDetail.Country,
                })
                .ToList();

            return new ReceiptsDTO
            {
                LocationCode = receipt.LocationCode,
                Address = $"{receipt.Street} {receipt.Number}",
                TimeStamp = DateTime.ParseExact($"{receipt.Date} {receipt.Time}", "yyyy-MM-dd HH:mm", null,
                    DateTimeStyles.None),
                ShipmentCode = receipt.ShipmentCode,
                ShipmentDetails = shipmentDetails,
                ServiceProvider = receipt.ServiceProvider,
                Status = receipt.Status
            };
        }
    }
}