using Models;
using System.Text.Json;

namespace MockFinancialInstitutionWeb.Api_Clients
{
    public class CreditServiceClient
    {
        private readonly HttpClient _httpClient;

        public CreditServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<long> GetCurrentCreditScore()
        {
            var response = await _httpClient.GetAsync("v1/getCurrentCreditScore");
            response.EnsureSuccessStatusCode();
            var creditScoreString = await response.Content.ReadAsStringAsync();

            try
            {
                var result = JsonSerializer.Deserialize<CreditScoreModel>(creditScoreString);

                return result.CreditScore;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
