using System.Text.Json.Serialization;

namespace Models
{
    public class AppraisalModel
    {
        [JsonPropertyName("appraisal")]
        public string Appraisal { get; set; }
    }
}
