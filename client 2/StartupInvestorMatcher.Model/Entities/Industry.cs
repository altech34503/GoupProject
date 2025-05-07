using System.Text.Json.Serialization;

namespace StartupInvestorMatcher.Model.Entities
{
    public class Industry
    {
        [JsonPropertyName("industry_id")]
        public int IndustryId { get; set; }

        [JsonPropertyName("industry_name")]
        public string? IndustryName { get; set; }
    }
}
