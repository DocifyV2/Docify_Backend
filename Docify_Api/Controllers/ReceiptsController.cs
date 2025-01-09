using System.Globalization;
using System.Text.Json;
using Azure.Storage.Blobs;
using Docify_Api.DTO;
using Docify_Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PostDigitaliser.AiDataClient.Models;
using PostDigitaliser.Server.DTO;
using PostDigitaliser.Server.Repository.Interfaces;

namespace Docify_Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ReceiptsController(ILogger<ReceiptsController> logger, BlobServiceClient blobContainerClient,
            IReceiptsService receiptsService)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await receiptsService.GetReceipts();

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
                var result = await receiptsService.GetReceipt(id);

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
                //Do some ai logic to get the data from the image

                //this is for testing purposes
                return Ok(await receiptsService.GetReceipt(Guid.Parse("22222222-2222-2222-2222-222222222222")));
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{GetType().Name} - Failed with message: {e.Message}");
                return StatusCode(500, e);
            }
        }

        [HttpPost("SubmitData")]
        public async Task<IActionResult> Post([FromForm] ShippingReceiptsInputModel postModel)
        {
            try
            {
                var test = JsonSerializer.Deserialize<List<ShippingReceiptsDetailsInputModel>>(
                    postModel.ShipmentDetails, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });
                return Ok(test);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{GetType().Name} - Failed with message: {e.Message}");
                return StatusCode(500, e.Message);
            }
        }
    }
}