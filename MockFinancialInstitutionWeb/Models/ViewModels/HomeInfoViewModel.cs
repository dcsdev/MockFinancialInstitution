using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace MockFinancialInstitutionWeb.Models.ViewModels
{
    public class HomeInfoViewModel
    {
        [JsonPropertyName("creditscore")]
        public long CreditScore { get; set; }
        [JsonPropertyName("appraisal")]
        public string HomeAppraisalValue { get; set; }

        public string HealthCheckV1 { get; set; }
        public string HealthCheckV2 { get; set; }
        public string HealthCheckV3 { get; set; }

        public string DocumentUploadResult { get; set; }
    }
}
