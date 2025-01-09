using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using PostDigitaliser.AiDataClient.Models;
using PostDigitaliser.Server.DTO;
using PostDigitaliser.Server.Wrappers.Interface;

namespace PostDigitaliser.Server.Wrappers
{
    public class GptApiWrapper : IGptApiWrapper
    {
        private readonly HttpClient _httpClient = new()
        {
            BaseAddress = new Uri("http://192.168.1.25:5000")
        };

        public async Task<IEnumerable<ReceiptsDTO>> GetJsonFromImage()
        {
            var response = await _httpClient.GetAsync("get-json");

            //Parse response to receiptModel
            var responseString = await response.Content.ReadAsStringAsync();

            var receiptModel = JsonSerializer.Deserialize<IEnumerable<ReceiptsDTO>>(responseString,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() } // Voeg deze regel toe
                });

            if (receiptModel == null)
            {
                throw new InvalidOperationException("Failed to deserialize response");
            }

            return receiptModel;
        }

        public async Task<bool> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Image file is null or empty", nameof(imageFile));
            }

            // Gebruik MultipartFormDataContent voor een POST-verzoek met bestand
            using var content = new MultipartFormDataContent();

            // Voeg bestand toe aan inhoud
            using var fileContent = new StreamContent(imageFile.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(imageFile.ContentType);

            content.Add(fileContent, "file", imageFile.FileName);

            // Voer de POST-uit
            var response = await _httpClient.PostAsync("", content);

            // Controleer of het verzoek succesvol was
            return response.IsSuccessStatusCode;
        }
    }
}