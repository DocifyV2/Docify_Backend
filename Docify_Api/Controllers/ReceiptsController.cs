using System.Globalization;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using PostDigitaliser.AiDataClient.Models;
using PostDigitaliser.Server.DTO;
using PostDigitaliser.Server.Repository.Interfaces;

namespace PostDigitaliser.Server.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ReceiptsController(ILogger<ReceiptsController> logger, BlobServiceClient blobContainerClient,
            IReceiptsRepository receiptsRepository)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // var result = await gptApiWrapper.GetJsonFromImage();
                var receipts = await receiptsRepository.GetReceipts();

                var result = new List<ReceiptsInputModel>();

                foreach (var receipt in receipts)
                {
                    var shipmentDetails = receipt.ShipmentDetails
                        .Select(shipmentDetail => new ShipmentDetailsInputModel
                        {
                            Id = shipmentDetail.Id,
                            BarCode = shipmentDetail.BarCode,
                            PostalCode = shipmentDetail.PostalCode,
                            HouseNumber = shipmentDetail.HouseNumber,
                            City = shipmentDetail.City,
                            Country = shipmentDetail.Country,
                        })
                        .ToList();

                    result.Add(new ReceiptsInputModel
                    {
                        Id = receipt.Id,
                        LocationCode = receipt.LocationCode,
                        Address = $"{receipt.Street} {receipt.Number}",
                        Timestamp = DateTime.ParseExact($"{receipt.Date} {receipt.Time}", "yyyy-MM-dd HH:mm", null,
                            DateTimeStyles.None),
                        ShipmentCode = receipt.ShipmentCode,
                        ShipmentDetails = shipmentDetails,
                        ServiceProvider = receipt.ServiceProvider,
                        Status = receipt.Status
                    });
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{GetType().Name} - Failed with message: {e.Message}");
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var receipt = await receiptsRepository.GetReceipt(id);

                var shipmentDetails = receipt.ShipmentDetails
                    .Select(shipmentDetail => new ShipmentDetailsInputModel
                    {
                        Id = shipmentDetail.Id,
                        BarCode = shipmentDetail.BarCode,
                        PostalCode = shipmentDetail.PostalCode,
                        HouseNumber = shipmentDetail.HouseNumber,
                        City = shipmentDetail.City,
                        Country = shipmentDetail.Country,
                    })
                    .ToList();

                var result = new ReceiptsInputModel
                {
                    LocationCode = receipt.LocationCode,
                    Address = $"{receipt.Street} {receipt.Number}",
                    Timestamp = DateTime.ParseExact($"{receipt.Date} {receipt.Time}", "yyyy-MM-dd HH:mm", null,
                        DateTimeStyles.None),
                    ShipmentCode = receipt.ShipmentCode,
                    ShipmentDetails = shipmentDetails,
                    ServiceProvider = receipt.ServiceProvider,
                    Status = receipt.Status
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{GetType().Name} - Failed with message: {e.Message}");
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PostReceipt postModel)
        {
            try
            {
                return Ok(Guid.Parse("22222222-2222-2222-2222-222222222222"));
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{GetType().Name} - Failed with message: {e.Message}");
                return StatusCode(500, e);
            }
        }
    }
}