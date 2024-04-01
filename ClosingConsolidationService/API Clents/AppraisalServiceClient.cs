using Models;
using System.Text.Json;

namespace MockFinancialInstitutionWeb.Api_Clients
{
    public class AppraisalServiceClient
    {
        private readonly HttpClient _httpClient;
        public AppraisalServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCurrentAppraisalValue()
        {
            try
            {
                var response = await _httpClient.GetAsync("v1/getCurrentAppraisalValue");
                response.EnsureSuccessStatusCode();
                var appraisalValueResult = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<AppraisalModel>(appraisalValueResult);
                return result.Appraisal;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<String> UploadDocument()
        {
            return "Uploaded";
        }

    }
}
