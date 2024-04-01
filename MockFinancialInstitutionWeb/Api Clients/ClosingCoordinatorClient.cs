using Microsoft.AspNetCore.Authorization;
using MockFinancialInstitutionWeb.Models.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace MockFinancialInstitutionWeb.Api_Clients
{
    public class ClosingCoordinatorClient
    {
        private readonly HttpClient _httpClient;

        public ClosingCoordinatorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [Authorize]
        public async Task<HealthCheck> HealthCheckV1()
        {
            var response = await _httpClient.GetAsync("v1/healthCheck");
            response.EnsureSuccessStatusCode();
            var healthResult = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<HealthCheck>(healthResult);
        }

        [Authorize]
        public async Task<HealthCheck> HealthCheckV2()
        {
            var response = await _httpClient.GetAsync("v2/healthCheck");
            response.EnsureSuccessStatusCode();
            var healthResult = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<HealthCheck>(healthResult);
        }

        [Authorize]
        public async Task<HealthCheck> HealthCheckV3()
        {
            var response = await _httpClient.GetAsync("v3/healthCheck");
            response.EnsureSuccessStatusCode();
            var healthResult = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<HealthCheck>(healthResult);
        }

        [Authorize]
        public async Task<HomeInfoViewModel> RetreiveCustomerFile()
        {
            var response = await _httpClient.GetAsync("v1/retrieveCustomerFile");
            response.EnsureSuccessStatusCode();
            var retrieveCustomerFileResult = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<HomeInfoViewModel>(retrieveCustomerFileResult);
            return result;
        }

        [Authorize]
        public async Task<string> UploadDocument(IFormFile file)
        {
            var requestUri = "v1/postDocument";

            using var content = new MultipartFormDataContent();
            using var fileStream = file.OpenReadStream();
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"file\"", FileName = "\"" + file.FileName + "\""
            };

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            content.Add(fileContent);

            var response = await _httpClient.PostAsync(requestUri, content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
