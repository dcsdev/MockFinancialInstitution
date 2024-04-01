using System.Text.Json.Serialization;

namespace MockFinancialInstitutionWeb.Models.ViewModels
{
    public class HealthCheck
    {
        [JsonPropertyName("status")]
        public string status { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime timestamp { get; set; }
        [JsonPropertyName("uptime")]
        public long Uptime { get; set; }
        [JsonPropertyName("memoryUsage")]
        public long MemoryUsage { get; set; }
    }
}
