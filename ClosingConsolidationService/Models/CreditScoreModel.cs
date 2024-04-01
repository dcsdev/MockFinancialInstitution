using System.Text.Json.Serialization;

namespace Models
{
    public class CreditScoreModel
    {
        [JsonPropertyName("creditScore")]
        public long CreditScore { get; set; }
    }
}
